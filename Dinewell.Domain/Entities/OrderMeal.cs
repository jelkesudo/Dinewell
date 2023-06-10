using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class OrderMeal : Entity
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual RestaurantMenu Meal { get; set; }
        public virtual ICollection<OrderMealSide> Sides { get; set; } = new List<OrderMealSide>();
    }
}
