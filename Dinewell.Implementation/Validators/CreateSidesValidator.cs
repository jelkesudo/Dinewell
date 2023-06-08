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
    public class CreateSidesValidator : AbstractValidator<CreateSideDto>
    {
        public CreateSidesValidator(DinewellContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Side name cannot be empty.")
                .MinimumLength(2).WithMessage("Minimum 2 characters are required for side name.")
                .Must(x => !context.Sides.Any(s => s.Name.ToLower() == x.ToLower())).WithMessage(x => $"{x.Name} as a side already exists.");
        }
    }
}
