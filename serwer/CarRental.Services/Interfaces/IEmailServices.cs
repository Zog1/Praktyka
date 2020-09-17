using CarRental.Services.Models.User;

namespace CarRental.Services.Interfaces
{
    public interface IEmailServices
    {
         bool EmailAfterRegistration(CreateUserDto createUserDto);
    }
}
