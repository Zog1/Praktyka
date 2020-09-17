using CarRental.DAL.Entities;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface ILocationRepository : IRepositoryBase<Location>
    {
        Task<Location> GetActualLocationByReservationIdAsync(int id);
    }
}
