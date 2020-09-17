using CarRental.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UsersDto>> GetAllUsersAsync();
        Task<UsersDto> GetUserAsync(int Id);
        Task<bool> DeleteUserAsync(int Id);
        Task<UsersDto> UpdateUserAsync(UsersDto Id);
    }
}
