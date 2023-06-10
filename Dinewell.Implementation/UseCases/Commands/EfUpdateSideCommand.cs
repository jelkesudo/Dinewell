using Dinewell.Application.Exceptions;
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
    public class EfUpdateSideCommand : EfUseCase, IUpdateSideCommand
    {
        private UpdateSideValidator _validator;
        public EfUpdateSideCommand(DinewellContext context, UpdateSideValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 26;

        public string Name => "Update food category (EF)";

        public string Description => "Updates food category from database.";

        public void Execute(UpdateSideDTO request)
        {
            _validator.ValidateAndThrow(request);

            var side = Context.Sides.Find(request.Id);

            if (side == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(Side));
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                side.Name = request.Name;
            }

            Context.Entry(side).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
