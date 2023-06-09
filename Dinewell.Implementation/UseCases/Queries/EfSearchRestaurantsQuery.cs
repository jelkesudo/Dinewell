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
    public class EfSearchRestaurantsQuery : EfUseCase, ISearchRestaurantsQuery
    {
        public EfSearchRestaurantsQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Search Restaurants (EF)";

        public string Description => "Use Entity Framework to search the restaurants.";

        public PagedResponse<RestaurantDTO> Execute(RestaurantSearch search)
        {
            var query = Context.Restaurants.AsQueryable();

            if (search.Name != null)
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if (search.WorkFrom != 0)
            {
                query = query.Where(x => x.WorkFrom == search.WorkFrom);
            }

            if (search.WorkTo != 0)
            {
                query = query.Where(x => x.WorkTo == search.WorkTo);
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

            var response = new PagedResponse<RestaurantDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new RestaurantDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Address = $"{x.Address} {x.AddressNumber}",
                WorkingHours = $"{x.WorkFrom}:00 - {x.WorkTo}:00",
                FoodCategories = x.FoodCategories.Select(f => new FoodCategoriesDTO
                {
                    Id = f.FoodCategory.Id,
                    Name = f.FoodCategory.Name,
                    Meals = f.Foods.Select(m => new MealsDTO
                    {
                        Id = m.Food.Id,
                        Name = m.Food.Name,
                        Description = m.Description,
                        Price = m.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => m.Id == p.FoodId).FirstOrDefault().Price,
                    }).ToList(),
                    Sides = f.Sides.Select(s => new SidesDTO
                    {
                        Id = s.Side.Id,
                        Name = s.Side.Name,
                        Price = s.SidePrices.OrderByDescending(p => p.PriceDate).Where(p => s.Id == p.SideId).FirstOrDefault().Price,
                    }).ToList(),
                }),
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
