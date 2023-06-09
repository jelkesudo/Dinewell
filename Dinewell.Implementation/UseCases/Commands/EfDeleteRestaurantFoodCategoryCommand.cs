using Dinewell.Application.Exceptions;
using Dinewell.Application;
using Dinewell.Application.UseCases.Commands;
using Dinewell.DataAccess;
using Dinewell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Commands
{
    public class EfDeleteRestaurantFoodCategoryCommand : EfUseCase, IDeleteRestaurantFoodCategoryCommand
    {
        private IApplicationActor _actor;
        public EfDeleteRestaurantFoodCategoryCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 35;

        public string Name => "Delete Restaurant Meal (EF)";

        public string Description => "";

        public void Execute(int request)
        {
            var query = Context.RestaurantFoodCategories.Find(request);

            if (query == null)
            {
                throw new EntityNotFoundException(request, nameof(RestaurantFoodCategory));
            }

            query.IsActive = false;
            query.DeletedAt = DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
