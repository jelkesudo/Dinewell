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
    public class EfSearchSpecificRestaurantSidesQuery : EfUseCase, ISearchSpecificRestaurantSidesQuery
    {
        public EfSearchSpecificRestaurantSidesQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 17;

        public string Name => "Get specific restaurant food sides (EF)";

        public string Description => "Gets specific restaurant and food category and its sides";

        public RestaurantAllSidesDTO Execute(int search)
        {
            var query = Context.RestaurantFoodCategories.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(FoodCategory));
            }

            return new RestaurantAllSidesDTO
            {
                Id = query.Id,
                RestaurantName = query.Restaurant.Name,
                FoodCategoryName = query.FoodCategory.Name,
                Sides = query.Sides.Select(x => new SidesDTO
                {
                    Id = x.Id,
                    Name = x.Side.Name,
                    Price = x.SidePrices.OrderByDescending(p => p.PriceDate).Where(p => x.Id == p.SideId).FirstOrDefault().Price
                })
            };
        }
    }
}
