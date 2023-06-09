using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases;
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
    public class EfSearchSpecificFoodCategoryQuery : EfUseCase, ISearchSpecificFoodCategoryQuery
    {
        public EfSearchSpecificFoodCategoryQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 8;

        public string Name => "Search specific food category (EF)";

        public string Description => "Searches specific food category";

        public SingleFoodCategoriyDTO Execute(int search)
        {
            var query = Context.RestaurantFoodCategories.AsQueryable();

            if (query == null)
            {
                throw new EntityNotFoundException(search, nameof(FoodCategory));
            }

            var foodCategories = query.Select(x => new SingleFoodCategoriyDTO
            {
                Id = x.FoodCategory.Id,
                Name = x.FoodCategory.Name,
                Meals = Context.RestaurantMenus
                    .Where(f => f.RestaurantFoodCategory.FoodCategory.Name == x.FoodCategory.Name)
                    .Select(f => new MealsDTO
                    {
                        Id = f.Food.Id,
                        Name = f.Food.Name,
                    })
                    .ToList()
            }).ToList();

            var distinctFoodCategories = foodCategories.GroupBy(x => x.Name)
                .Select(g => g.First())
                .ToList();

            if (!distinctFoodCategories.Any(x => x.Id == search))
            {
                throw new EntityNotFoundException(search, nameof(FoodCategory));
            }

            return distinctFoodCategories.Where(x => x.Id == search).First();
        }
    }
}
