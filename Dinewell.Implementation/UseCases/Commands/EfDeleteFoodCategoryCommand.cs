using Dinewell.Application;
using Dinewell.Application.Exceptions;
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
    public class EfDeleteFoodCategoryCommand : EfUseCase, IDeleteFoodCategoryCommand
    {
        private IApplicationActor _actor;
        public EfDeleteFoodCategoryCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 31;

        public string Name => "Delete food category (EF)";

        public string Description => "";

        public void Execute(int request)
        {
            var query = Context.FoodCategories.Find(request);

            if (query == null)
            {
                throw new EntityNotFoundException(request, nameof(FoodCategory));
            }

            query.IsActive = false;
            query.DeletedAt = DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
