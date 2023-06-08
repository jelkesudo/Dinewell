using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class RestaurantMenu : Entity
    {
        public int RestaurantFoodCategoryId { get; set; }
        public int FoodId { get; set; }
        public string Description { get; set; }
        public virtual RestaurantFoodCategory RestaurantFoodCategory { get; set; }
        public virtual Food Food { get; set; }
        public virtual RestaurantMenuImage RestaurantMenuImage { get; set; }
        public virtual ICollection<MenuPrice> MenuPrices { get; set; } = new List<MenuPrice>();
        public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public virtual ICollection<OrderMeal> OrderMeals { get; set; } = new List<OrderMeal>();
    }
}
