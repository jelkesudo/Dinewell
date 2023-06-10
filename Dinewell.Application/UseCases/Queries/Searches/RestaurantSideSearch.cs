using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class RestaurantSideSearch : PagedSearch
    {
        public string RestaurantName { get; set; }
        public string FoodCategoryName { get; set; }
        public string SideName { get; set; }
    }
}
