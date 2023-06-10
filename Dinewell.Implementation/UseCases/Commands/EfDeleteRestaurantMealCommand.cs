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
    public class EfDeleteRestaurantMealCommand : EfUseCase, IDeleteRestaurantMealCommand
    {
        private IApplicationActor _actor;
        public EfDeleteRestaurantMealCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 18;

        public string Name => "Delete Restaurant Meal (EF)";

        public string Description => "";

        public void Execute(int request)
        {
            var query = Context.RestaurantMenus.Find(request);

            if(query == null)
            {
                throw new EntityNotFoundException(request, nameof(RestaurantMenu));
            }

            query.IsActive = false;
            query.DeletedAt = DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
