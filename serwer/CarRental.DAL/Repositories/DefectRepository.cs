using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class DefectRepository:RepositoryBase<Defect>,IDefectRepository
    {
        public DefectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Defect> FindDefectByIdAsync(int id)
        {
            return await context.Set<Defect>()
                .FirstOrDefaultAsync(e => e.DefectId == id);
        }
        public async Task<IEnumerable<Defect>> FindAllDefectsAsync()
        {
            return await context.Set<Defect>()
                .ToListAsync();
        }
    }
}
