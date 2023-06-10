using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class SingleFoodCategoriyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SeparateMealDTO> Meals { get; set; } = new List<SeparateMealDTO>();

    }

    public class SeparateMealDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
