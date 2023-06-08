using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public int WorkFrom { get; set; }
        public int WorkTo { get; set; }
        public virtual RestaurantImage RestaurantImage { get; set; }
        public virtual ICollection<RestaurantFoodCategory> FoodCategories { get; set; } = new List<RestaurantFoodCategory>();
    }
}
