using System;

namespace CarRental.Services.Models.User
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public string EncdodePassword { get; set; }
        public int? RoleOfWorker { get; set; }
        public string CodeOfVerification { get; set; }

    }
}
