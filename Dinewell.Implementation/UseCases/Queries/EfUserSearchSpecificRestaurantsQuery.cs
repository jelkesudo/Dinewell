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
    public class EfUserSearchSpecificRestaurantsQuery : UseCases.EfUseCase, IUserSearchSpecificRestaurantsQuery
    {
        public EfUserSearchSpecificRestaurantsQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 6;

        public string Name => "";

        public string Description => "";

        public RestaurantDTO Execute(int search)
        {
            var query = Context.Restaurants.FirstOrDefault(x => x.IsActive && x.Id == search);
            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(Restaurant));
            }

            return new RestaurantDTO
            {
                Id = query.Id,
                Name = query.Name,
                Description = query.Description,
                Address = $"{query.Address} {query.AddressNumber}",
                WorkingHours = $"{query.WorkFrom}:00 - {query.WorkTo}:00",
                FoodCategories = query.FoodCategories.Select(f => new FoodCategoriesDTO
                {
                    Id = f.FoodCategory.Id,
                    Name = f.FoodCategory.Name,
                    Meals = f.Foods.Where(x => x.IsActive).Select(m => new MealsDTO
                    {
                        Id = m.Food.Id,
                        Name = m.Food.Name,
                        Description = m.Description,
                        Price = m.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => m.Id == p.FoodId && p.IsActive).FirstOrDefault().Price,
                    }).ToList(),
                    Sides = f.Sides.Where(x => x.IsActive).Select(s => new SidesDTO
                    {
                        Id = s.Side.Id,
                        Name = s.Side.Name,
                        Price = s.SidePrices.OrderByDescending(p => p.PriceDate).Where(p => s.Id == p.SideId && p.IsActive).FirstOrDefault().Price,
                    }).ToList(),
                }),
            };
        }
    }
}
