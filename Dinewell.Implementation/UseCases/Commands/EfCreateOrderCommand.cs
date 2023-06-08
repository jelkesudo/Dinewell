using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using Dinewell.Implementation.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfCreateOrderCommand : EfUseCase, ICreateOrderCommand
    {
        private CreateOrderValidator _validator;
        public EfCreateOrderCommand(DinewellContext context, CreateOrderValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 22;

        public string Name => "Add new order (EF)";

        public string Description => "Adds new order with its meals and sides";

        public void Execute(CreateOrderDTO request)
        {
            _validator.ValidateAndThrow(request);

            // Create a new Order entity
            var order = new Order
            {
                UserId = request.UserId, 
                OrderNumber = Guid.NewGuid()
            };

            foreach (var mealDto in request.Meals)
            {
                var orderMeal = new OrderMeal
                {
                    Order = order,
                    MealId = mealDto.MealId
                };

                order.OrderMeals.Add(orderMeal);

                foreach (var sideDto in mealDto.Sides)
                {
                    var orderSide = new OrderMealSide
                    {
                        OrderMeal = orderMeal,
                        SideId = sideDto.SideId
                    };

                    orderMeal.Sides.Add(orderSide);
                }
            }
            Context.Orders.Add(order);
            Context.SaveChanges();
        }
    }
}
