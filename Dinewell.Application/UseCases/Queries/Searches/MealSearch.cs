using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class MealSearch : PagedSearch
    {
        public string Name { get; set; }
        public string Restaurant { get; set; }
        public string FoodCategory { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public bool HasDiscount { get; set; } = false;
    }
}
