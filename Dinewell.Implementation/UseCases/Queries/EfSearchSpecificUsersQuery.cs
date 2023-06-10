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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dinewell.Implementation.UseCases.Queries
{
    public class EfSearchSpecificUsersQuery : UseCases.EfUseCase, ISearchSpecificUsersQuery
    {
        public EfSearchSpecificUsersQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 42;

        public string Name => "Search specific user (EF)";

        public string Description => "";

        public UserDTO Execute(int search)
        {
            var query = Context.Users.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(User));
            }

            return new UserDTO
            {
                Id = query.Id,
                FullName = $"{query.FirstName} {query.LastName}",
                Username = query.Username,
                Email = query.Email,
                Address = query.Address == null ? "" : $"{query.Address.Name} {query.Address.Number}"
            };
        }
    }
}
