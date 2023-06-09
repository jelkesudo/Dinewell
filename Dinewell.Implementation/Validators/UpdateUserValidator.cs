using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator(DinewellContext context)
        {
            var regexFirstLastName = @"^\b([A-ZÀ-ÿ][-,a-z. ']+[ ]*)+$";

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("First name must not be empty.")
                .Matches(regexFirstLastName).WithMessage("First name must begin with capital letter (John)")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters").When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Last name must not be empty.")
                .Matches(regexFirstLastName).WithMessage("Last name must begin with capital letter (Doe), and be between 3 and 50 characters long.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters").When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Username).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Username must not be empty.")
                .Matches(@"^[A-ZČĆŠĐŽa-zčćžđš0-9\s]{4,20}$").WithMessage("Username must be at least 4 and between 20 characters long.")
                .Must(x => !context.Users.Any(u => u.Username == x)).WithMessage(x => $"Username {x.Username} is already in use.").When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email must not be empty.")
                .EmailAddress().WithMessage("Email must be in the correct format (example: name@gmail.com).")
                .Must(x => !context.Users.Any(u => u.Email == x)).WithMessage(x => $"Email {x.Email} is already in use.").When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Password must not be empty.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password must contain a capital letter, one symbole and a number, and be at least 8 characters long.").When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address must be inserted").When(x => !string.IsNullOrEmpty(x.Address) || x.AddressNumber != 0);

            RuleFor(x => x.AddressNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address number must be inserted").When(x => !string.IsNullOrEmpty(x.Address));
        }
    }
}
