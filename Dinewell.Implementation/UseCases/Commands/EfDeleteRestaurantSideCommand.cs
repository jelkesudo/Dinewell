using Dinewell.Application;
using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfDeleteRestaurantSideCommand : EfUseCase, IDeleteRestaurantSideCommand
    {
        private IApplicationActor _actor;
        public EfDeleteRestaurantSideCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 19;

        public string Name => "Delete restaurant side (EF)";

        public string Description => "";

        public void Execute(int request)
        {
            var query = Context.RestaurantSides.Find(request);

            if (query == null)
            {
                throw new EntityNotFoundException(request, nameof(RestaurantSide));
            }

            query.IsActive = false;
            query.DeletedAt = DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
