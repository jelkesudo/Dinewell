using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfCreateRestaurantMenuCommand : EfUseCase, ICreateRestaurantMenuCommand
    {
        private CreateRestaurantMenuValidator _validator;
        public EfCreateRestaurantMenuCommand(DinewellContext context, CreateRestaurantMenuValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 13;

        public string Name => "Create new meal (EF)";

        public string Description => "Creates new meal for the certain restaurant and food category";

        public void Execute(CreateMealDTO request)
        {
            _validator.ValidateAndThrow(request);

            var newMeal = new RestaurantMenu
            {
                RestaurantFoodCategoryId = request.RestaurantFoodCategoryId,
                FoodId = request.FoodId,
                Description = request.Ingredients,
            };

            var newPrice = new MenuPrice
            {
                Price = request.Price,
                Food = newMeal,
            };

            Context.RestaurantMenus.Add(newMeal);
            Context.MenuPrices.Add(newPrice);
            if (request.Discount != 0)
            {
                var newDiscount = new Discount
                {
                    Value = request.Discount,
                    DateFrom = request.DiscountFrom,
                    DateTo = request.DiscountTo,
                    Food = newMeal,
                };
                Context.Discounts.Add(newDiscount);
            }
            Context.SaveChanges();
        }
    }
}
