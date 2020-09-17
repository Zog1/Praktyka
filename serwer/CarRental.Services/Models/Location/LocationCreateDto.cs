namespace CarRental.Services.Models.Location
{
    public class LocationCreateDto
    {
        public int ReservationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
