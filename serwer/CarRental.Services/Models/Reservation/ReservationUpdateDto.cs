using System;

namespace CarRental.Services.Models.Reservation
{
    public class ReservationUpdateDto
    {
        public int ReservationId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsFinished { get; set; }
        public int CarId { get; set; }
    }
}
