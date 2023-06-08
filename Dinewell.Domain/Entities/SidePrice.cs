using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class SidePrice : Entity
    {
        public decimal Price { get; set; }
        public DateTime PriceDate { get; set; }
        public int SideId {get; set;}
        public virtual RestaurantSide RestaurantSide {get; set;}
    }
}
