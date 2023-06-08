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
    public class UpdateFoodCategoryValidator : AbstractValidator<UpdateFoodCategoryDTO>
    {
        public UpdateFoodCategoryValidator(DinewellContext context)
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name must not be empty.")
           .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
           .MaximumLength(50).WithMessage("Name must have at least 50 characters.")
           .Must(x => !context.FoodCategories.Any(f => f.Name.ToLower() == x.ToLower())).WithMessage(x => $"Food Category {x.Name} already exists in the database.").When(x => !string.IsNullOrEmpty(x.Name));
        }
    }
}
