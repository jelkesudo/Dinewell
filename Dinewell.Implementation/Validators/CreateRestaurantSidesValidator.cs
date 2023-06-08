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
    public class CreateRestaurantSidesValidator : AbstractValidator<CreateRestaurantSideDTO>
    {
        private DinewellContext _context;
        public CreateRestaurantSidesValidator(DinewellContext context)
        {
            _context = context;

            RuleFor(x => x.RestaurantFoodCategoryId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("RestaurantFoodCategoryId must not be empty")
               .Must(x => context.RestaurantFoodCategories.Any(r => r.Id == x)).WithMessage(x => $"RestaurantFoodCategory with id {x.RestaurantFoodCategoryId} does not exist, consider making it first.");

            RuleFor(x => x.SideId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("RestaurantFoodCategoryId must not be empty")
                .Must(x => context.Sides.Any(r => r.Id == x)).WithMessage(x => $"Side with id {x.SideId} does not exist, consider making it first.");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Price must not be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => new { x.RestaurantFoodCategoryId, x.SideId })
            .Must(HaveUniquePair)
            .WithMessage("Meal with this restaurantfoodcategoryid and sideid already exist");
        }

        public bool HaveUniquePair(CreateRestaurantSideDTO dto, dynamic ids)
        {
            return !_context.RestaurantSides.Any(x => x.RestaurantFoodCategoryId == dto.RestaurantFoodCategoryId && x.SideId == dto.SideId);
        }
    }
}
