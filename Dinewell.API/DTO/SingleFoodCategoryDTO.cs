using System.Collections.Generic;

namespace Dinewell.API.DTO
{
    public class SingleFoodCategoriyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MealsDTO> Meals { get; set; } = new List<MealsDTO>();

    }
}
