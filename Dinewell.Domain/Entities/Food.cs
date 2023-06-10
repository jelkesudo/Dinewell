using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class Food : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<RestaurantMenu> Menus { get; set; } = new List<RestaurantMenu>();
    }
}
