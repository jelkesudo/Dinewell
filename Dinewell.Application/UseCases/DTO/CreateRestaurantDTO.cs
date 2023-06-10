using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class CreateRestaurantDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public int WorkFrom { get; set; }
        public int WorkTo { get; set; }
    }
}
