using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class RestaurantAllSidesDTO
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string FoodCategoryName { get; set; }
        public IEnumerable<SidesDTO> Sides { get; set; } = new List<SidesDTO>();
    }
}
