using CarRental.DAL.Entities;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IRefreshRepository:IRepositoryBase<RefreshToken>
    {
        public  Task<RefreshToken> FindByRefreshTokenAsync(string refresh);
    }
}
