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
    public class EfSearchSpecificOrdersQuery : UseCases.EfUseCase, ISearchSpecificOrdersQuery
    {
        public EfSearchSpecificOrdersQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 38;

        public string Name => "Get Specific Order (EF)";

        public string Description => "Gets specific order by its id.";

        public OrderDTO Execute(int search)
        {
            var query = Context.Orders.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(Order));
            }

            return new OrderDTO
            {
                Id = query.Id,
                OrderNumber = query.OrderNumber.ToString(),
                OrderAddress = query.OrderAddress,
                Meals = query.OrderMeals.Select(m => new OrderMealsDTO
                {
                    Id = m.Id,
                    Name = m.Meal.Food.Name,
                    Description = m.Meal.Description,
                    Price = m.Meal.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => m.Meal.Id == p.FoodId && p.IsActive).FirstOrDefault().Price,
                    Quantity = m.Quantity,
                    Sides = m.Sides.Select(s => new OrderSidesDTO
                    {
                        Id = s.Id,
                        Name = s.Side.Side.Name,
                        Price = s.Side.SidePrices.OrderByDescending(p => p.PriceDate).FirstOrDefault(p => s.Side.Id == p.SideId && p.IsActive).Price
                    }).ToList()
                }).ToList()
            };
        }
    }
}
