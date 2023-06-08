using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class CreateRestaurantMenuValidator : AbstractValidator<CreateMealDTO>
    {
        private readonly DinewellContext _context;
        public CreateRestaurantMenuValidator(DinewellContext context)
        {
            _context = context;
            RuleFor(x => x.RestaurantFoodCategoryId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("RestaurantFoodCategoryId must not be empty")
                .Must(x => context.RestaurantFoodCategories.Any(r => r.Id == x)).WithMessage(x => $"RestaurantFoodCategory with id {x.RestaurantFoodCategoryId} does not exist, consider making it first.");

            RuleFor(x => x.FoodId).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("RestaurantFoodCategoryId must not be empty")
                .Must(x => context.Foods.Any(r => r.Id == x)).WithMessage(x => $"Food with id {x.FoodId} does not exist, consider making it first.");

            RuleFor(x => x.Ingredients).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Ingredients must not be empty").MinimumLength(10).WithMessage("Minimal length for ingredients must be 10 characters");

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Price must not be empty").Must(isValidNumer).WithMessage("Price must be a number").GreaterThan(4.99m).WithMessage("Minimal price must be at least 5");

            RuleFor(x => x.Discount).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Price must not be empty")
                .GreaterThan(0).WithMessage("Discount must be more than 0")
                .LessThanOrEqualTo(100).WithMessage("Discount must be less than 101")
                .When(x => x.Discount != 0);

            RuleFor(x => x.DiscountFrom).Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage("DiscountFrom is required")
             .GreaterThan(DateTime.UtcNow)
            .WithMessage("DiscountFrom must be greater than current UTC date and time")
            .When(x => x.Discount != 0);

            RuleFor(x => x.DiscountTo).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("DiscountTo is required")
            .GreaterThan(x => x.DiscountFrom)
            .WithMessage("DiscountTo must be greater than DateFrom")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("DiscountTo must be greater than current UTC date and time")
            .When(x => x.Discount != 0);

            RuleFor(x => new { x.RestaurantFoodCategoryId, x.FoodId })
            .Must(HaveUniquePair)
            .WithMessage("Meal with this restaurantfoodcategoryid and foodid already exist");
        }

        public bool isValidNumer(decimal number)
        {
            if (number % 1 != 0)
            {
                return true;
            }

            int integerValue = (int)number;
            return (integerValue == number);
        }

        public bool HaveUniquePair(CreateMealDTO dto, dynamic ids)
        {
            return !_context.RestaurantMenus.Any(x => x.RestaurantFoodCategoryId == dto.RestaurantFoodCategoryId && x.FoodId == dto.FoodId);
        }
    }
}
