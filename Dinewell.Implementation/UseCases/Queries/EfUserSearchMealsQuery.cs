using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Queries
{
    public class EfUserSearchMealsQuery : UseCases.EfUseCase, IUserSearchMealsQuery
    {
        public EfUserSearchMealsQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "User search meals (EF)";

        public string Description => "";

        public PagedResponse<MealSeparateDTO> Execute(MealSearch search)
        {
            var query = Context.RestaurantMenus.AsQueryable();

            if (search.Name != null)
            {
                query = query.Where(x => x.Food.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if (search.Restaurant != null)
            {
                query = query.Where(x => x.RestaurantFoodCategory.Restaurant.Name.ToLower().Contains(search.Restaurant.ToLower()));
            }

            if (search.FoodCategory != null)
            {
                query = query.Where(x => x.RestaurantFoodCategory.FoodCategory.Name.ToLower().Contains(search.Restaurant.ToLower()));
            }

            if (search.FoodCategory != null)
            {
                query = query.Where(x => x.RestaurantFoodCategory.FoodCategory.Name.ToLower().Contains(search.Restaurant.ToLower()));
            }

            if (search.PriceFrom > 0)
            {
                query = query.Where(x => x.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => x.Id == p.FoodId && p.IsActive).FirstOrDefault().Price > search.PriceFrom);
            }

            if (search.PriceTo > 0)
            {
                query = query.Where(x => x.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => x.Id == p.FoodId && p.IsActive).FirstOrDefault().Price < search.PriceTo);
            }

            if (search.HasDiscount)
            {
                query = query.Where(x => x.Discounts.Any(p => p.DateFrom > DateTime.UtcNow && p.DateTo < DateTime.UtcNow && p.IsActive));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<MealSeparateDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Where(x => x.IsActive).Select(x => new MealSeparateDTO
            {
                Id = x.Id,
                Name = x.Food.Name,
                RestaurantId = x.RestaurantFoodCategory.Restaurant.Id,
                RestaurantName = x.RestaurantFoodCategory.Restaurant.Name,
                FoodCategoryId = x.RestaurantFoodCategory.FoodCategory.Id,
                FoodCategoryName = x.RestaurantFoodCategory.FoodCategory.Name,
                Price = x.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => x.Id == p.FoodId && p.IsActive).FirstOrDefault().Price,
                Discount = x.Discounts.OrderByDescending(p => p.CreatedAt).Where(p => p.DateFrom > DateTime.UtcNow && p.DateTo < DateTime.UtcNow && p.IsActive).Select(p => p.Value).FirstOrDefault(),
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
