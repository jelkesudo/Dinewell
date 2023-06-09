using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfCreateFoodCategoryCommand : EfUseCase, ICreateFoodCategoryCommand
    {
        private CreateFoodCategoryValidator _validator;
        public EfCreateFoodCategoryCommand(DinewellContext context, CreateFoodCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 9;

        public string Name => "Create Food Category (Ef)";

        public string Description => "Creates new food category";

        public void Execute(CreateFoodCategoryDTO request)
        {
            _validator.ValidateAndThrow(request);

            var newFoodCategory = new FoodCategory
            {
                Name = request.Name,
            };

            Context.FoodCategories.Add(newFoodCategory);

            Context.SaveChanges();
        }
    }
}
