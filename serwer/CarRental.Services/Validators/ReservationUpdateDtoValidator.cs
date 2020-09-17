using CarRental.Services.Models.Reservation;
using FluentValidation;
using System;

namespace CarRental.Services.Validators
{
    public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateDto>
    {
        public ReservationUpdateDtoValidator()
        {
            RuleFor(p => p.CarId).NotEmpty().GreaterThan(0);
            RuleFor(p => p.RentalDate).GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(p => p.ReturnDate).GreaterThan(p => p.RentalDate);
        }
    }
}
