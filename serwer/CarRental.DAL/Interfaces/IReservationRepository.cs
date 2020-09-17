using CarRental.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IReservationRepository : IRepositoryBase<Reservation>
    {
        Task<IEnumerable<Reservation>> FindAllByUserIdAsync(int id);
        Task<IEnumerable<Reservation>> FindAllByCarIdAsync(int id);
        Task DeleteAsync(int id);
        Task<List<Reservation>> FilterReservationsAsync(Reservation reservation);
    }
}
