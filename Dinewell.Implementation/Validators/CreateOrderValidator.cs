using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDTO>
    {
        private DinewellContext _context;
        public CreateOrderValidator(DinewellContext context)
        {
            _context = context;
            RuleFor(x => x.UserAddress).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address cannot be empty");
            RuleForEach(x => x.Meals)
            .ChildRules(meal =>
            {
                meal.RuleFor(m => m.MealId)
                    .Must(MealExistInDatabase)
                    .WithMessage(m => $"Meal with id {m.MealId}  does not exist in the database.");

                meal.RuleFor(m => m.Quantity)
                    .NotEmpty().WithMessage("Quantity must not be empty")
                    .GreaterThanOrEqualTo(1).WithMessage("Quantity must be more than 1");

                meal.RuleForEach(m => m.Sides)
                .ChildRules(side =>
                {
                    side.RuleFor(s => s.SideId)
                        .Must(SideExistInDatabase)
                        .WithMessage(s => $"Side with id {s.SideId} does not exist in the database.");
                }).When(side => side.Sides.Count != 0);
            });
        }

        private bool MealExistInDatabase(int id)
        {
            return _context.RestaurantMenus.Any(m => m.Id == id && m.IsActive);
        }
        private bool SideExistInDatabase(int id)
        {
            return _context.RestaurantSides.Any(m => m.Id == id && m.IsActive);
        }
    }
}
