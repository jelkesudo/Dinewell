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
    public class EfSearchSpecificFoodsQuery : EfUseCase, ISearchSpecificFoodsQuery
    {
        public EfSearchSpecificFoodsQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "Search specific foods (EF)";

        public string Description => "";

        public FoodSeparateDTO Execute(int search)
        {
            var query = Context.Foods.Find(search);

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(Food));
            }


            return new FoodSeparateDTO
            {
                Id = query.Id,
                Name = query.Name,
            };
        }
    }
}
