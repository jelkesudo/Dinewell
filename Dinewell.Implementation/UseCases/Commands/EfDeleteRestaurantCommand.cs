using Dinewell.Application;
using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.Commands;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfDeleteRestaurantCommand : EfUseCase, IDeleteRestaurantCommand
    {
        private IApplicationActor _actor;
        public EfDeleteRestaurantCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 16;

        public string Name => "Delete restaurant (EF)";

        public string Description => "Soft deletes restaurant from users view.";

        public void Execute(int request)
        {
            var query = Context.Restaurants.Find(request);

            if (query == null)
            {
                throw new EntityNotFoundException(request, nameof(Restaurant));
            }

            query.IsActive = false;
            query.DeletedAt= DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
