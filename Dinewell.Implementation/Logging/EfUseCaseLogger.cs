using Dinewell.Application.Logging;
using Dinewell.DataAccess;
using Dinewell.Implementation.UseCases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.Logging
{
    public class EfUseCaseLogger : EfUseCase, IUseCaseLogger
    {
        public EfUseCaseLogger(DinewellContext context) : base(context)
        {
        }

        public void Add(UseCaseLogEntry entry)
        {
            Context.LogEntries.Add(new Domain.Entities.LogEntry
            {
                Actor = entry.Actor,
                ActorId = entry.ActorId,
                UseCaseData = JsonConvert.SerializeObject(entry.Data),
                UseCaseName = entry.UseCaseName,
                CreatedAt = DateTime.UtcNow
            });

            Context.SaveChanges();
        }
    }
}
