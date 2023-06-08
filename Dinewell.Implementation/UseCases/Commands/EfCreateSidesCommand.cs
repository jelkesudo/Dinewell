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
    public class EfCreateSidesCommand : EfUseCase, ICreateSidesCommand
    {
        private readonly CreateSidesValidator _validator;
        public EfCreateSidesCommand(DinewellContext context, CreateSidesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "Create side (EF)";

        public string Description => "Create new side in the database with entity framework";

        public void Execute(CreateSideDto request)
        {
            _validator.ValidateAndThrow(request);

            var newSide = new Side
            {
                Name = request.Name
            };

            Context.Sides.Add(newSide);
            Context.SaveChanges();
        }
    }
}
