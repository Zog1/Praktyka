using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IRepositoryBase <T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindByIdAsync(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
