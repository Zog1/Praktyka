namespace CarRental.Services.Models.Location
{
    public class LocationDto
    {
        public int LocationId { get; set; }
        public int ReservationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActual { get; set; }
    }
}
