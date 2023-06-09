﻿using Dinewell.Application.UseCases.DTO;
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
    public class EfSearchFoodCategoriesQuery : EfUseCase, ISearchFoodCategoriesQuery
    {
        public EfSearchFoodCategoriesQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 5;

        public string Name => "Get Food Categories (EF)";

        public string Description => "Get all distinct food categories and foods with parameter name.";

        public PagedResponse<FoodCategoriesDTO> Execute(FoodCategorySearch search)
        {
            var query = Context.RestaurantFoodCategories.AsQueryable();

            if (search.Name != null)
            {
                query = query.Where(x => x.FoodCategory.Name.ToLower().Contains(search.Name.ToLower()));
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

            var response = new PagedResponse<FoodCategoriesDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new FoodCategoriesDTO
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

            response.Data = response.Data.GroupBy(x => x.Name)
                .Select(g => g.First())
                .ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}