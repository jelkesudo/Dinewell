using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class CreateRestaurantFoodCategoryDTO
    {
        public int RestaurantId { get; set; }
        public int FoodCategoryId { get; set; }
    }
}
