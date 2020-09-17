using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Defect;

namespace CarRental.Services.Mapper
{
    public class DefectProfile : Profile
    {
        public DefectProfile()
        {
            CreateMap<Defect, DefectDto>();
        }
    }
}
