using CarRental.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> FindByIdDetailsAsync(int id);
        Task<IEnumerable<User>> FindAllUsersAsync();
        Task<User> FindByCodeOfVerificationAsync(string code);
        Task<User> FindByLoginAsync(string email);
    }
}
