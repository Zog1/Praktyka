using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Models.User;

namespace CarRental.Services.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
           CreateMap<User, CreateUserDto>();
           
            CreateMap<User,UsersDto>().ReverseMap();
        }
    }
}
