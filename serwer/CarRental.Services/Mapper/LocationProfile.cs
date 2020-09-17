using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Location;

namespace CarRental.Services.Mapper
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationDto>();
        }
    }
}
