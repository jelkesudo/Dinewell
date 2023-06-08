using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class FoodCategory : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<RestaurantFoodCategory> Restaurants { get; set; } = new List<RestaurantFoodCategory>();
    }
}
