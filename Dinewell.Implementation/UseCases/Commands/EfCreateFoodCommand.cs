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
    public class EfCreateFoodCommand : EfUseCase, ICreateFoodCommand
    {
        private CreateFoodValidator _validator;
        public EfCreateFoodCommand(DinewellContext context, CreateFoodValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 38;

        public string Name => "Create new Food (EF)";

        public string Description => "";

        public void Execute(CreateFoodDTO request)
        {
            _validator.ValidateAndThrow(request);

            var newFood = new Food
            {
                Name = request.Name
            };

            Context.Foods.Add(newFood);
            Context.SaveChanges();
        }
    }
}
