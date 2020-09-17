using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByIdDetailsAsync(int id)
        {
            return await context.Set<User>()
                .Where(e => e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.UserId == id);
        }

        public async Task<IEnumerable<User>> FindAllUsersAsync()
        {
            return await context.Set<User>()
                .Where(e => e.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<User> FindByCodeOfVerificationAsync(string code)
        {
            return await context.Set<User>()
                .Where(e => e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.CodeOfVerification == code);
        }

        public async Task<User> FindByLoginAsync(string email)
        {
            return await context.Set<User>()
                .Where(e => e.IsDeleted == false)
                .FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
