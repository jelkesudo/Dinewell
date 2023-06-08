using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class Discount : Entity
    {
        public int Value { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int FoodId { get; set; }
        public virtual RestaurantMenu Food { get; set; }

    }
}
