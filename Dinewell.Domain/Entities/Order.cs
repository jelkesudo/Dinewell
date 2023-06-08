using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class Order : Entity
    {
        public Guid OrderNumber { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderMeal> OrderMeals { get; set; } = new List<OrderMeal>();
    }
}
