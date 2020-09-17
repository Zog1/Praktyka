using CarRental.Services.Models.Defect;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IDefectsService
    {
         Task<DefectDto> RegisterDefectAsync(RegisterDefectDto registerDefectDto);
         Task<IEnumerable<DefectDto>> GetAllDefectsAsync();
         Task<DefectDto> GetDefectAsync(int Id);
         Task<DefectDto> UpdateDefectAsync(UpdateDefectDto updateDefectDto);
    }
}
