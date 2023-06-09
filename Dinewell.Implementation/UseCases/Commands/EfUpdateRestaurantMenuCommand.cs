using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfUpdateRestaurantMenuCommand : EfUseCase, IUpdateRestaurantMenuCommand
    {
        private UpdateRestaurantMealValidator _validator;
        public EfUpdateRestaurantMenuCommand(DinewellContext context, UpdateRestaurantMealValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 28;

        public string Name => "Update restaurant meal (EF)";

        public string Description => "Update the data for restaurant meal.";

        public void Execute(UpdateRestaurantMealDTO request)
        {
            _validator.ValidateAndThrow(request);

            var updateMeal = Context.RestaurantMenus.Find(request.Id);

            if (updateMeal == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(RestaurantMenu));
            }

            if (!string.IsNullOrEmpty(request.Ingredients))
            {
                updateMeal.Description = request.Ingredients;
            }
            if (request.Price != 0)
            {
                var newPrice = new MenuPrice
                {
                    Price = request.Price,
                    Food = updateMeal,
                };
                Context.MenuPrices.Add(newPrice);
            }
            
            if (request.Discount != 0)
            {
                var hasDiscounts = Context.Discounts.OrderByDescending(x => x.CreatedAt).Where(x => x.FoodId == request.Id).FirstOrDefault();
                if (hasDiscounts != null)
                {
                    hasDiscounts.IsActive = false;
                }
                var newDiscount = new Discount
                {
                    Value = request.Discount,
                    DateFrom = request.DiscountFrom,
                    DateTo = request.DiscountTo,
                    Food = updateMeal,
                };
                Context.Discounts.Add(newDiscount);  
            }

            Context.Entry(updateMeal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
