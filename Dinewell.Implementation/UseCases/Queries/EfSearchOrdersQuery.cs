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
    public class EfSearchOrdersQuery : EfUseCase, ISearchOrdersQuery
    {
        public EfSearchOrdersQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "Get Orders (EF)";

        public string Description => "Get all orders from the database";

        public PagedResponse<OrderDTO> Execute(OrderSearch search)
        {
            var query = Context.Orders.AsQueryable();

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<OrderDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new OrderDTO
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber.ToString(),
                OrderAddress = x.OrderAddress,
                Meals = x.OrderMeals.Select(m => new OrderMealsDTO
                {
                    Id = m.Id,
                    Name = m.Meal.Food.Name,
                    Description = m.Meal.Description,
                    Price = m.Meal.MenuPrices.OrderByDescending(p => p.PriceDate).Where(p => m.Meal.Id == p.FoodId && p.IsActive).FirstOrDefault().Price,
                    Quantity = m.Quantity,
                    Sides = m.Sides.Select(s => new OrderSidesDTO
                    {
                        Id = s.Side.Side.Id,
                        Name = s.Side.Side.Name,
                        Price = s.Side.SidePrices.OrderByDescending(p => p.PriceDate).FirstOrDefault(p => s.Side.Id == p.SideId && p.IsActive).Price
                    }).ToList()
                }).ToList()
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
