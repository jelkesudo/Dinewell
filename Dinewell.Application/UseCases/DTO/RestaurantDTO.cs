using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string WorkingHours { get; set; }
        public IEnumerable<FoodCategoriesDTO> FoodCategories { get; set; } = new List<FoodCategoriesDTO>();
    }

    public class FoodCategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MealsDTO> Meals { get; set; } = new List<MealsDTO>();
        public IEnumerable<SidesDTO> Sides { get; set; } = new List<SidesDTO>();

    }

    public class MealsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
    }

    public class SidesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
