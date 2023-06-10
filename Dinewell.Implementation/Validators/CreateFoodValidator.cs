using Dinewell.Application.UseCases.Commands;
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
    public class CreateFoodValidator : AbstractValidator<CreateFoodDTO>
    {
        public CreateFoodValidator(DinewellContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Side name cannot be empty.")
               .MinimumLength(2).WithMessage("Minimum 2 characters are required for side name.")
               .Must(x => !context.Foods.Any(s => s.Name.ToLower() == x.ToLower())).WithMessage(x => $"{x.Name} as a food already exists.");
        }
    }
}
