using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class SideSearch : PagedSearch
    {
        public string Name { get; set; }
    }
}
