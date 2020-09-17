using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class TermService : ITermService
    {
        private readonly IReservationRepository reservationRepository;
        public TermService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Get days in which car with given id can be rent. 
        /// Method can be called only with cars id and returns free days for next two weeks or
        /// can be called with cars id and the term we are interested in 
        /// and returns all free days from a week before given rental date to week after given return date.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns list of available days converted to strings.</returns>
        public async Task<IEnumerable<string>> GetFreeTermsByCarIdAsync(int id, DateTime? rentalDate, DateTime? returnDate)
        {
            var reservations = await reservationRepository.FindAllByCarIdAsync(id);
            List<int> freeDays = PrepareFreeDaysArray(rentalDate, returnDate);
            freeDays = RemoveUnavailableDates(reservations, freeDays);
            return GetConvertedDates(freeDays);
        }

        /// <summary>
        /// Get array with days converted to DayOfYear. Days in array depends on the fact, if dates were given. 
        /// If yes, method returns free days for next two weeks. 
        /// If no,method returns all free days from a week before given rental date to week after given return date.
        /// </summary>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>List with days given as DayOfYear value.</returns>
        public List<int> PrepareFreeDaysArray(DateTime? rentalDate, DateTime? returnDate)
        {
            int week = 7;
            var startRange
                = (rentalDate == null || rentalDate.Value.DayOfYear - week < DateTime.Now.DayOfYear) ?
                DateTime.Now.DayOfYear : rentalDate.Value.DayOfYear - week;
            var endRange = returnDate == null ? 2 * week : returnDate.Value.DayOfYear - rentalDate.Value.DayOfYear + week + 1;
            var freeDays = Enumerable.Range(startRange, endRange).ToList();
            return freeDays;
        }

        /// <summary>
        /// Remove days in which the car we are interested in is not available.
        /// </summary>
        /// <param name="reservations"></param>
        /// <param name="freeDays"></param>
        /// <returns>List with days given as DayOfYear value when car is not already rent.</returns>
        public List<int> RemoveUnavailableDates(IEnumerable<Reservation> reservations, List<int> freeDays)
        {
            foreach (var reservation in reservations)
            {
                for (int i = reservation.RentalDate.DayOfYear; i <= reservation.ReturnDate.DayOfYear; i++)
                {
                    freeDays.Remove(i);
                }
            }
            return freeDays;
        }

        /// <summary>
        /// Convert dates from DayOfYear value to strings with format dd/MM/yyyy.
        /// </summary>
        /// <param name="freeDays"></param>
        /// <returns></returns>
        public IEnumerable<string> GetConvertedDates(List<int> freeDays)
        {
            var dates = new List<string>();
            foreach (var dayOfYear in freeDays)
            {
                var date = new DateTime(DateTime.Now.Year, 1, 1).AddDays(dayOfYear - 1).Date.ToString("dd/MM/yyyy");
                dates.Add(date);
            }
            return dates;
        }

        /// <summary>
        /// Check if dates are given in a correct order. 
        /// </summary>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns true is dates are correct and false otherwise.</returns>
        public bool DatesAreCorrect(DateTime rentalDate, DateTime returnDate)
        {
            return rentalDate < returnDate && rentalDate.Date >= DateTime.Now.Date;
        }

        /// <summary>
        /// Check if dates have null values.
        /// </summary>
        /// <param name="rentalDate"></param>
        /// <param name="returnDate"></param>
        /// <returns>Returns true if dates aren't nulls and false otherwise.</returns>
        public bool DatesHaveValue(DateTime? rentalDate, DateTime? returnDate)
        {
            return rentalDate.HasValue && returnDate.HasValue;
        }
    }
}
