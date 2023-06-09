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
    public class UpdateFoodValidator : AbstractValidator<UpdateFoodDTO>
    {
        public UpdateFoodValidator(DinewellContext context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Food name cannot be empty.")
               .MinimumLength(2).WithMessage("Minimum 2 characters are required for food name.")
               .Must(x => !context.Foods.Any(s => s.Name.ToLower() == x.ToLower())).WithMessage(x => $"{x.Name} as a food already exists.").When(x => !string.IsNullOrEmpty(x.Name)).When(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}
