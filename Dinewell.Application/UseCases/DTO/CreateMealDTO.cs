using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class CreateMealDTO
    {
        public int RestaurantFoodCategoryId { get; set; }
        public int FoodId { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public DateTime DiscountTo { get; set; }
        public DateTime DiscountFrom { get; set; }
    }
}
