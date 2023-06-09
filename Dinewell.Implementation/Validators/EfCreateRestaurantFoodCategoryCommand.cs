using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class EfCreateRestaurantFoodCategoryCommand : EfUseCase, ICreateRestaurantFoodCategoryCommand
    {
        private CreateRestaurantFoodCategoriesValidator _validator;
        public EfCreateRestaurantFoodCategoryCommand(DinewellContext context, CreateRestaurantFoodCategoriesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "Create RestaurantFoodCategory (EF)";

        public string Description => "Create a specific food category for a restaurant";

        public void Execute(CreateRestaurantFoodCategoryDTO request)
        {
            _validator.ValidateAndThrow(request);

            var newRestaurantFoodCategory = new RestaurantFoodCategory
            {
                RestaurantId = request.RestaurantId,
                FoodCategoryId = request.FoodCategoryId,
            };

            Context.RestaurantFoodCategories.Add(newRestaurantFoodCategory);
            Context.SaveChanges();
        }
    }
}
