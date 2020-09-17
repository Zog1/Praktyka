using System;

namespace CarRental.DAL.Entities
{
    public class Defect : BaseEntity
    {
        public int DefectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RegistrationNumber{get;set;}
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReport { get; set; } 
        public int CarId { get; set; }
        public Status Status { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
        public Defect(int userId ,int carId ,string name,string surname , string mobilenumber, string registrationNumber,
                                string description,Status status)
        {
            UserId = userId;
            CarId = carId;
            Name = name;
            Surname = surname;
            PhoneNumber = mobilenumber;
            RegistrationNumber =registrationNumber ;
            Description = description;
            DateOfReport = DateTime.Now;
            Status = status;

        }
        public Defect() { }
        public void Update(string description, Status status)
        {
            Description = description;
            Status = status;
            DateModified = DateTime.Now;
        }
    }
 
}
