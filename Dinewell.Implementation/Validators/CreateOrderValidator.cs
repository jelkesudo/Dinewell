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
            RuleForEach(dto => dto.Meals)
            .ChildRules(meal =>
            {
                meal.RuleFor(m => m.MealId)
                    .Must(MealExistInDatabase)
                    .WithMessage(m => $"Meal with id {m.MealId}  does not exist in the database.");

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
            return _context.RestaurantMenus.Any(m => m.Id == id);
        }
        private bool SideExistInDatabase(int id)
        {
            return _context.RestaurantSides.Any(m => m.Id == id);
        }
    }
}
