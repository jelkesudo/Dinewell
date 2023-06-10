using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfUpdateRestaurantCommand : EfUseCase, IUpdateRestaurantCommand
    {
        private UpdateRestaurantValidator _validator;
        public EfUpdateRestaurantCommand(DinewellContext context, UpdateRestaurantValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 23;

        public string Name => "Update Restaurant (EF)";

        public string Description => "Update restaurant from database.";

        public void Execute(UpdateRestaurantDTO request)
        {
            _validator.ValidateAndThrow(request);

            var restaurant = Context.Restaurants.Find(request.Id);

            if (restaurant == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(Restaurant));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                restaurant.Name = request.Name;
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                restaurant.Description = request.Description;
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                restaurant.Address = request.Address;
            }
            if (request.AddressNumber != 0)
            {
                restaurant.AddressNumber = request.AddressNumber;
            }
            if (request.WorkFrom != 0)
            {
                restaurant.WorkFrom = request.WorkFrom;
            }
            if (request.WorkTo != 0)
            {
                restaurant.WorkTo = request.WorkTo;
            }
            Context.Entry(restaurant).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
