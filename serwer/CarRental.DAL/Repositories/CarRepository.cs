using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Car>> FindAllAsync()
        {
            return await context.Cars
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetReservedCarsByDatesAsync(DateTime rentalDate, DateTime returnDate)
        {
            var entities = await context.Reservations
                 .Where(p => p.IsFinished == false)
                 .Where(p => p.ReturnDate >= rentalDate && p.RentalDate <= returnDate)
                 .Include(p => p.Car)
                 .ToListAsync();
            var cars = entities
                .GroupBy(p => p.Car)
                .Select(p => p.Key)
                .ToList();
            return cars;
        }
    }
}
