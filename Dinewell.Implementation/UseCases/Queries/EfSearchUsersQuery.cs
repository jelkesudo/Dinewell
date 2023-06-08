using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Queries
{
    public class EfSearchUsersQuery : EfUseCase, ISearchUsersQuery
    {
        public EfSearchUsersQuery(DinewellContext context) : base(context)
        {
        }
        public int Id => 3;

        public string Name => "Search users (EF)";

        public string Description => "Query for searching users";

        public PagedResponse<UserDTO> Execute(UserSearch search)
        {
            var query = Context.Users.AsQueryable();

            if (search.FirstName != null)
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }

            if (search.LastName != null)
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }

            if (search.Email != null)
            {
                query = query.Where(x => x.Email.ToLower().Contains(search.Email.ToLower()));
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

            var response = new PagedResponse<UserDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new UserDTO
            {
                Id = x.Id,
                FullName = $"{x.FirstName} {x.LastName}",
                Username = x.Username,
                Email = x.Email,
                Address = x.Address.Name == null ? "" : $"{x.Address.Name} {x.Address.Number}"
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
