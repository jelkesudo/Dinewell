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
    public class EfCreateRestaurantSideCommand : EfUseCase, ICreateRestaurantSideCommand
    {
        private CreateRestaurantSidesValidator _validator;
        public EfCreateRestaurantSideCommand(DinewellContext context, CreateRestaurantSidesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "Create restaurant side (EF)";

        public string Description => "Add a side for the specific food category of a restaurant";

        public void Execute(CreateRestaurantSideDTO request)
        {
            _validator.ValidateAndThrow(request);

            var restaurantSide = new RestaurantSide
            {
                RestaurantFoodCategoryId = request.RestaurantFoodCategoryId,
                SideId = request.SideId,
            };
            var sidePrice = new SidePrice
            {
               RestaurantSide = restaurantSide,
               Price = request.Price
            };
            Context.RestaurantSides.Add(restaurantSide);
            Context.SidePrices.Add(sidePrice);
            Context.SaveChanges();
        }
    }
}
