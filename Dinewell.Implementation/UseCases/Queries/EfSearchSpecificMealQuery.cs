using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Queries
{
    public class EfSearchSpecificMealQuery : UseCases.EfUseCase, ISearchSpecificMealQuery
    {
        public EfSearchSpecificMealQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "Get specific meal (EF)";

        public string Description => "Gets the specific meal from the database";

        public MealSeparateDTO Execute(int search)
        {
            var query = Context.RestaurantMenus.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(RestaurantMenu));
            }

            return new MealSeparateDTO
            {
                Id = query.Id,
                Name = query.Food.Name,
                RestaurantId = query.RestaurantFoodCategory.Restaurant.Id,
                RestaurantName = query.RestaurantFoodCategory.Restaurant.Name,
                FoodCategoryId = query.RestaurantFoodCategory.FoodCategory.Id,
                FoodCategoryName = query.RestaurantFoodCategory.FoodCategory.Name,
                Price = query.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => query.Id == p.FoodId && p.IsActive).FirstOrDefault().Price,
                Discount = query.Discounts.OrderByDescending(p => p.CreatedAt).Where(p => p.DateFrom > DateTime.UtcNow && p.DateTo < DateTime.UtcNow && p.IsActive).Select(p => p.Value).FirstOrDefault(),
            };
        }
    }
}
