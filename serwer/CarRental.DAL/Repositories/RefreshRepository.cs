using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class RefreshRepository : RepositoryBase<RefreshToken> , IRefreshRepository
    {
        public RefreshRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public  async Task<RefreshToken> FindByRefreshTokenAsync(string refresh)
        {
            return await context.Set<RefreshToken>()
                .Where(e=>e.IsValid==true)
                .FirstOrDefaultAsync(e => e.Refresh == refresh);
        }
    }
}
