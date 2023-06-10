using Dinewell.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        public EfUseCase(DinewellContext context)
        {
            Context = context;
        }
        protected DinewellContext Context { get; }
    }
}
