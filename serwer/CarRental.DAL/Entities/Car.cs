using System;

namespace CarRental.DAL.Entities
{
    public class Car : BaseEntity
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public CarType TypeOfCar { get; set; }
        public int NumberOfDoor { get; set; }
        public int NumberOfSits { get; set; }
        public int YearOfProduction { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(string brand, string model, string registrationNumber, CarType type, int numberOfDoor,
           int numberOfSits, int yearOfProduction, string imagePath)
        {
            Brand = brand;
            Model = model;
            RegistrationNumber = registrationNumber;
            TypeOfCar = type;
            NumberOfDoor = numberOfDoor;
            NumberOfSits = numberOfSits;
            YearOfProduction = yearOfProduction;
            ImagePath = imagePath;
            DateModified = DateTime.Now;
        }
    }
}
