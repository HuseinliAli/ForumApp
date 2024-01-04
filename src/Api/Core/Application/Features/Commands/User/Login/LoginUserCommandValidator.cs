using Common.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.User.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("{PropertyName} should at least be {MinLength} characters");
        }
    }
}
