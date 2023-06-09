using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class OrderSearch : PagedSearch
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
