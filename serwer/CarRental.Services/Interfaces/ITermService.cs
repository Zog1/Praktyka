using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface ITermService
    {
        Task<IEnumerable<string>> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate);
        bool DatesAreCorrect(DateTime rentalDate, DateTime returnDate);
        bool DatesHaveValue(DateTime? rentalDate, DateTime? returnDate);
    }
}
