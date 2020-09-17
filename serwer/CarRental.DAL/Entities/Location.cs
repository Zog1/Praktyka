namespace CarRental.DAL.Entities
{
    public class Location : BaseEntity
    {
        public int LocationId { get; set; }
        public int ReservationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActual { get; set; }

        public Reservation Reservation { get; set; }
    }
}
