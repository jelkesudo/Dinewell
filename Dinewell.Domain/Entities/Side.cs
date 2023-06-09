using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class Side : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<RestaurantSide> RestaurantFoodCategories { get; set; } = new List<RestaurantSide>();
    }
}
