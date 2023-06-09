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
    public class EfSearchSidesQuery : EfUseCase, ISearchSidesQuery
    {
        public EfSearchSidesQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 13;

        public string Name => "Get Sides (EF)";

        public string Description => "Get all sides from the database";

        public PagedResponse<SidesSeparateDTO> Execute(SideSearch search)
        {
            var query = Context.Sides.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name));
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

            var response = new PagedResponse<SidesSeparateDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Select(x => new SidesSeparateDTO
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
