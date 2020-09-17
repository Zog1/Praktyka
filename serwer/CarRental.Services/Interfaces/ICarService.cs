using CarRental.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarDto> CreateCarAsync(CarCreateDto car);
        Task<IEnumerable<CarDto>> GetAllCarsAsync();
        Task<CarDto> GetCarByIdAsync(int id);
        Task<CarDto> UpdateCarAsync(CarDto car);
        Task DeleteCarAsync(int id);
        Task<IEnumerable<CarDto>> GetAvailableCars(DateTime rentalDate, DateTime returnDate);
    }
}
