using CarRental.DAL.Entities;

namespace CarRental.Services.Models.Car
{
    public class CarCreateDto
    {
        public string Brand { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }

        public CarType TypeOfCar { get; set; }
        public int NumberOfDoor { get; set; }
        public int NumberOfSits { get; set; }
        public int YearOfProduction { get; set; }
        public string ImagePath { get; set; }
    }
}
