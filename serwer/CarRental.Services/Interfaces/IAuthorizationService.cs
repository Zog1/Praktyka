using CarRental.Services.Models.Token;
using CarRental.Services.Models.User;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<CreateUserDto> RegistrationUserAsync(CreateUserDto createUserDto);
        Task<bool> SetPasswordAsync(UpdateUserPasswordDto updateUserPassword);
        Task<TokenDto> SignInAsync(UserLoginDto userLogin);
    }
}
