using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class CreateRestaurantSideDTO
    {
        public int RestaurantFoodCategoryId { get; set; }
        public int SideId { get; set; }
        public decimal Price { get; set; }
    }
}
