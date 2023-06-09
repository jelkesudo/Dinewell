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
    public class EfSearchFoodsQuery : EfUseCase, ISearchFoodsQuery
    {
        public EfSearchFoodsQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 36;

        public string Name => "Search food categories (EF)";

        public string Description => "";

        public PagedResponse<FoodSeparateDTO> Execute(SearchFoodDTO search)
        {
            var query = Context.Foods.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name == search.Name);
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

            var response = new PagedResponse<FoodSeparateDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new FoodSeparateDTO
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
