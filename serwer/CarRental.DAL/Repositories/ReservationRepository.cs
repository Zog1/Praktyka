using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        { }

        public new async Task<IEnumerable<Reservation>> FindAllAsync()
        {
            var result = await context
                .Reservations
                .Include(p => p.Car)
                .Include(p => p.User)
                .Include(p => p.User)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Reservation>> FindAllByUserIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.UserId == id)
                .Include(p => p.Car)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Reservation>> FindAllByCarIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.IsFinished == false)
                .Where(p => p.CarId == id)
                .Include(p => p.Car)
                .ToListAsync();
            return result;
        }

        public new async Task<Reservation> FindByIdAsync(int id)
        {
            var result = await context.Reservations
                .Where(p => p.ReservationId == id)
                .Include(p => p.Car)
                .SingleOrDefaultAsync();
            return result;
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id);
            entity.IsFinished = true; 
        }

        public async Task<List<Reservation>> FilterReservationsAsync(Reservation reservation)
        {
            return await context.Reservations
                .Where(p => p.CarId == reservation.CarId)
                .Where(p => p.IsFinished == false)
                .Where(p =>
                (p.RentalDate < reservation.ReturnDate && p.ReturnDate > reservation.RentalDate)
                || (p.RentalDate < reservation.RentalDate && p.ReturnDate > reservation.RentalDate)
                || (p.RentalDate >= reservation.RentalDate && p.ReturnDate <= reservation.ReturnDate))
                .ToListAsync();
        }
    }
}
