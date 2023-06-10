using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class OrderMealSide : Entity
    {
        public int OrderMealId { get; set; }
        public int SideId { get; set; }
        public virtual OrderMeal OrderMeal { get; set; }
        public virtual RestaurantSide Side { get; set; }
    }
}
