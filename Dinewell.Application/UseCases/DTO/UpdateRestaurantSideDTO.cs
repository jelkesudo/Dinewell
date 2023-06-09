using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class UpdateRestaurantSideDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
}
