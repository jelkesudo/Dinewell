using Dinewell.Application.Exceptions;
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
    public class EfUpdateFoodCategoryCommand : EfUseCase, IUpdateFoodCategoryCommand
    {
        private UpdateFoodCategoryValidator _validator;
        public EfUpdateFoodCategoryCommand(DinewellContext context, UpdateFoodCategoryValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 21;

        public string Name => "Update food category (EF)";

        public string Description => "Updates food category from the database";

        public void Execute(UpdateFoodCategoryDTO request)
        {
            _validator.ValidateAndThrow(request);

            var foodCategory = Context.FoodCategories.Find(request.Id);

            if (foodCategory == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(FoodCategory));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                foodCategory.Name = request.Name;
            }

            Context.Entry(foodCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
