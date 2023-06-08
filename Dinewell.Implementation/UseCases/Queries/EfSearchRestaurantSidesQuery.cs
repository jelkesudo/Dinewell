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
    public class EfSearchRestaurantSidesQuery : EfUseCase, ISearchRestaurantSidesQuery
    {
        public EfSearchRestaurantSidesQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 16;

        public string Name => "Get Restaurant sides (Ef)";

        public string Description => "Get sides from restaurants and their food categories.";

        public PagedResponse<RestaurantAllSidesDTO> Execute(RestaurantSideSearch search)
        {
            var query = Context.RestaurantFoodCategories.AsQueryable();

            if (!string.IsNullOrEmpty(search.RestaurantName))
            {
                query = query.Where(x => x.Restaurant.Name.ToLower().Contains(search.RestaurantName));
            }

            if (!string.IsNullOrEmpty(search.FoodCategoryName))
            {
                query = query.Where(x => x.FoodCategory.Name.ToLower().Contains(search.FoodCategoryName));
            }

            if (!string.IsNullOrEmpty(search.FoodCategoryName))
            {
                query = query.Where(x => x.Sides.Select(s => s.Side.Name.ToLower().Contains(search.SideName)).FirstOrDefault());
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

            var response = new PagedResponse<RestaurantAllSidesDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Where(x => x.IsActive == true).Select(x => new RestaurantAllSidesDTO
            {
                Id = x.Id,
                RestaurantName = x.Restaurant.Name,
                FoodCategoryName = x.FoodCategory.Name,
                Sides = x.Sides.Select(x => new SidesDTO
                {
                    Id= x.Id,
                    Name = x.Side.Name,
                    Price = x.SidePrices.OrderByDescending(p => p.PriceDate).Where(p => x.Id == p.SideId).FirstOrDefault().Price
                })
            });

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
