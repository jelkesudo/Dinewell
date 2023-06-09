using Dinewell.Application.Emails;
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
    public class EfCreateRestaurantCommand : EfUseCase, ICreateRestaurantCommand
    {
        private CreateRestaurantValidator _validator;
        public EfCreateRestaurantCommand(DinewellContext context, CreateRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 1;

        public string Name => "Create Restaurant (EF)";

        public string Description => "Add a restaurant to the Restaurants table";

        public void Execute(CreateRestaurantDTO request)
        {
            _validator.ValidateAndThrow(request);

            var restaurant = new Restaurant
            {
                Name = request.Name,
                Description = request.Description,
                Address= request.Address,
                AddressNumber = request.AddressNumber,
                WorkFrom = request.WorkFrom,
                WorkTo = request.WorkTo,
            };

            Context.Restaurants.Add(restaurant);

            Context.SaveChanges();
        }
    }
}
