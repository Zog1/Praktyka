using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.Car;

namespace CarRental.Services.Mapper
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
        }
    }
}
