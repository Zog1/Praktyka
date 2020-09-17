using CarRental.Services.Models.Reservation;
using FluentValidation;
using System;

namespace CarRental.Services.Validators
{
    public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateDto>
    {
        public ReservationCreateDtoValidator()
        {
            RuleFor(p => p.CarId).NotNull().GreaterThan(0);
            RuleFor(p => p.UserId).NotNull().GreaterThan(0);
            RuleFor(p => p.RentalDate).GreaterThanOrEqualTo(DateTime.Now.Date)
                .NotEmpty().NotNull();
            RuleFor(p => p.ReturnDate.Date).GreaterThanOrEqualTo(p => p.RentalDate.Date)
                .NotEmpty().NotNull();
        }
    }
}
