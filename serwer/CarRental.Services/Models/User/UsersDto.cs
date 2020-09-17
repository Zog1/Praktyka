using CarRental.DAL.Entities;

namespace CarRental.Services.Models.User
{
    public class UsersDto : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool isValid { get; set; }
    }
}
