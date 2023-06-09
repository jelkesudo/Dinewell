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
    public class EfUpdateFoodCommand : EfUseCase, IUpdateFoodCommand
    {
        private UpdateFoodValidator _validator;
        public EfUpdateFoodCommand(DinewellContext context, UpdateFoodValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 39;

        public string Name => "Update food (EF)";

        public string Description => "";

        public void Execute(UpdateFoodDTO request)
        {
            _validator.ValidateAndThrow(request);

            var food = Context.Foods.Find(request.Id);

            if (food == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(Food));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                food.Name = request.Name;
            }

            Context.Entry(food).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
