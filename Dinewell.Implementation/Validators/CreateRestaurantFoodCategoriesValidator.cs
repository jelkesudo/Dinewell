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
    public class CreateRestaurantFoodCategoriesValidator : AbstractValidator<CreateRestaurantFoodCategoryDTO>
    {
        private DinewellContext _context;
        public CreateRestaurantFoodCategoriesValidator(DinewellContext context)
        {
            _context = context;

            RuleFor(x => x.RestaurantId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("RestaurantId must not be empty")
               .Must(x => context.Restaurants.Any(r => r.Id == x)).WithMessage(x => $"Restaurant with id {x.RestaurantId} does not exist, consider making it first.");

            RuleFor(x => x.FoodCategoryId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("FoodCategoryId must not be empty")
                .Must(x => context.FoodCategories.Any(r => r.Id == x)).WithMessage(x => $"Food category with id {x.FoodCategoryId} does not exist, consider making it first.");

            RuleFor(x => new { x.RestaurantId, x.FoodCategoryId })
            .Must(HaveUniquePair)
            .WithMessage("Restaurant with this food category already exists.");
        }

        public bool HaveUniquePair(CreateRestaurantFoodCategoryDTO dto, dynamic ids)
        {
            return !_context.RestaurantFoodCategories.Any(x => x.RestaurantId == dto.RestaurantId && x.FoodCategoryId == dto.FoodCategoryId);
        }
    }
}
