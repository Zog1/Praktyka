using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface ICarRepository : IRepositoryBase<Car>
    {
        Task<IEnumerable<Car>> GetReservedCarsByDatesAsync(DateTime rentalDate, DateTime returnDate);
    }
}
