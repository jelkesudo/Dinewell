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
    public class EfUpdateRestautantSideCommand : EfUseCase, IUpdateRestautantSideCommand
    {
        private UpdateRestaurantSideValidator _validator;
        public EfUpdateRestautantSideCommand(DinewellContext context, UpdateRestaurantSideValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "Update restaurant side (EF)";

        public string Description => "Update price for the side of the particular restaurants food category";

        public void Execute(UpdateRestaurantSideDTO request)
        {
            _validator.ValidateAndThrow(request);

            var updateSide = Context.RestaurantSides.Find(request.Id);

            if (updateSide == null)
            {
                throw new EntityNotFoundException(request.Id, nameof(RestaurantSide));
            }

            var hadPrice = Context.SidePrices.OrderByDescending(x => x.CreatedAt).Where(x => x.SideId == updateSide.Id).FirstOrDefault();

            if (hadPrice != null)
            {
                hadPrice.IsActive = false;
            }

            var sidePrice = new SidePrice
            {
                RestaurantSide = updateSide,
                Price = request.Price
            };

            Context.SidePrices.Add(sidePrice);
            Context.Entry(updateSide).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
