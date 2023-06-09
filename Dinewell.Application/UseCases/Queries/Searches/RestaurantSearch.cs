using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class RestaurantSearch : PagedSearch
    {
        public string Name { get; set; }
        public int WorkFrom { get; set; }
        public int WorkTo { get; set; }
    }
}
