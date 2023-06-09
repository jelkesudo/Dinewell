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
    public class EfSearchSpecificSideQuery : EfUseCase, ISearchSpecificSideQuery
    {
        public EfSearchSpecificSideQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 14;

        public string Name => "Search specific side (EF)";

        public string Description => "Search for the specific side in database";

        public SidesSeparateDTO Execute(int search)
        {
            var query = Context.Sides.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(Side));
            }

            return new SidesSeparateDTO
            {
                Id = query.Id,
                Name = query.Name,
            };
        }
    }
}
