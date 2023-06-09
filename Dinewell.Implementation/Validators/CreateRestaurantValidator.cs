using Dinewell.Application.UseCases.DTO;
using Dinewell.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Validators
{
    public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantDTO>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name must not be empty.")
                .MinimumLength(5).WithMessage("Name must have at least 5 characters.")
                .MaximumLength(50).WithMessage("Name must have at least 50 characters.");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description must not be empty.")
                .MinimumLength(10).WithMessage("Description must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Description can be 200 characters long.");

            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address must not be empty.")
                .MinimumLength(5).WithMessage("Address must have at least 10 characters.")
                .MaximumLength(200).WithMessage("Address can be 200 characters long.");

            RuleFor(x => x.AddressNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address number must not be empty.")
                .GreaterThanOrEqualTo(1).WithMessage("Address number cannot be less than 1");


            RuleFor(x => x.WorkFrom).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23");

            RuleFor(x => x.WorkTo).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("AddressNumber must not be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("Working hours from cannot be less than 1")
                .LessThanOrEqualTo(23).WithMessage("Working hours from cannot be more than 23");
        }
    }
}
