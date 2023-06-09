using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class RestaurantSide : Entity
    {
        public int RestaurantFoodCategoryId { get; set; }
        public int SideId { get; set; }
        public virtual RestaurantFoodCategory RestaurantFoodCategory { get; set; }
        public virtual Side Side { get; set; }
        public virtual ICollection<SidePrice> SidePrices { get; set; } = new List<SidePrice>();
        public virtual ICollection<OrderMealSide> OrderMeal { get; set; } = new List<OrderMealSide>();
    }
}
