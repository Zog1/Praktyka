using CarRental.Services.Models.Location;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ILocationService
    {
        Task<LocationDto> GetActualLocationByReservationIdAsync(int id);
        Task<LocationDto> GetLocationByIdAsync(int id);
        Task<LocationDto> CreateLocationAsync(LocationCreateDto locationCreateDto);
        Task DeleteLocationAsync(int id);
    }
}
