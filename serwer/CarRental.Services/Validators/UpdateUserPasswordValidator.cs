using CarRental.Services.Models.User;
using FluentValidation;

namespace CarRental.Services.Validators
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(p => p.EncodePassword).MinimumLength(8);
            RuleFor(p => p).Must(p => p.ConfirmEncodePassword == p.EncodePassword).WithMessage("Password is not the same");
        }
    }
}
