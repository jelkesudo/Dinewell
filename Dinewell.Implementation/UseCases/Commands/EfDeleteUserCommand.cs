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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        private IApplicationActor _actor;
        public EfDeleteUserCommand(DinewellContext context, IApplicationActor actor) : base(context)
        {
            _actor = actor;
        }

        public int Id => 20;

        public string Name => "Delete user (EF)";

        public string Description => "";

        public void Execute(int request)
        {
            var query = Context.Users.Find(request);

            if (query == null)
            {
                throw new EntityNotFoundException(request, nameof(User));
            }

            query.IsActive = false;
            query.DeletedAt = DateTime.Now;
            query.DeletedBy = _actor.Username;

            Context.SaveChanges();
        }
    }
}
