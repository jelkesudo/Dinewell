using Castle.Core.Internal;
using Dinewell.Application.UseCases.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class UpdateRestaurantValidator : AbstractValidator<UpdateRestaurantDTO>
    {
        public UpdateRestaurantValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name must not be empty.")
                .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
                .MaximumLength(50).WithMessage("Name must have at least 50 characters.").When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description must not be empty.")
                .MinimumLength(10).WithMessage("Description must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Description can be 200 characters long.").When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address must not be empty.")
                .MinimumLength(5).WithMessage("Address must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Address can be 200 characters long.").When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.AddressNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address number must not be empty.")
                .GreaterThanOrEqualTo(1).WithMessage("Address number cannot be less than 1").When(x => x.AddressNumber != 0);


            RuleFor(x => x.WorkFrom).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23").When(x => x.WorkFrom != 0);

            RuleFor(x => x.WorkTo).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23").When(x => x.WorkTo != 0);
        }
    }
}
