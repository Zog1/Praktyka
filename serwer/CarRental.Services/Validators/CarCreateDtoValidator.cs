using CarRental.Services.Models.Car;
using FluentValidation;
using System;
namespace CarRental.Services.Validators
{
    public class CarCreateDtoValidator : AbstractValidator<CarCreateDto>
    {
        public CarCreateDtoValidator()
        {
            RuleFor(p => p.Brand).NotEmpty().MaximumLength(30);
            RuleFor(p => p.Model).NotEmpty().MaximumLength(20);
            RuleFor(p => p.RegistrationNumber).NotEmpty().MaximumLength(7);
            RuleFor(p => p.NumberOfDoor).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(5);
            RuleFor(p => p.NumberOfSits).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(9);
            RuleFor(p => p.YearOfProduction).NotEmpty().GreaterThanOrEqualTo(1950).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(p => p.TypeOfCar).IsInEnum();
        }
    }
}
