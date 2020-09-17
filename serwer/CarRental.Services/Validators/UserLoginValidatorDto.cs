using CarRental.Services.Models.User;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class UserLoginValidatorDto : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidatorDto()
        {
            RuleFor(p => p.Email).NotEmpty().MinimumLength(5).EmailAddress();
            RuleFor(p => p.Password).NotEmpty().MinimumLength(5);
        }
    }
}
