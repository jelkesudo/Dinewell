using Dinewell.Application.UseCases.DTO;
using Dinewell.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class UpdateRestaurantSideValidator : AbstractValidator<UpdateRestaurantSideDTO>
    {
        private DinewellContext _context;
        public UpdateRestaurantSideValidator(DinewellContext context)
        {
            _context = context;

            RuleFor(x => x.Price).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Price must not be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
