using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class RestaurantFoodCategory : Entity
    {
        public int RestaurantId { get; set; }
        public int FoodCategoryId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual FoodCategory FoodCategory { get; set; }
        public virtual ICollection<RestaurantMenu> Foods { get; set; } = new List<RestaurantMenu>();
        public virtual ICollection<RestaurantSide> Sides { get; set; } = new List<RestaurantSide>();
    }
}
