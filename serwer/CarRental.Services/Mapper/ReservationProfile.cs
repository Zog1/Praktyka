using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Services.Formatters;
using CarRental.Services.Models.Reservation;

namespace CarRental.Services.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>()
                .ForMember(
                p => p.RentalDate, 
                opt => opt.ConvertUsing(new DateFormatter()))
                .ForMember(
                p => p.ReturnDate,
                opt => opt.ConvertUsing(new DateFormatter()));
        }
    }
}
