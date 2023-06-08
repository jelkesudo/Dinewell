using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class MenuPrice : Entity
    {
        public decimal Price { get; set; }
        public DateTime PriceDate { get; set; }
        public int FoodId { get; set; }
        public virtual RestaurantMenu Food { get; set; }
    }
}
