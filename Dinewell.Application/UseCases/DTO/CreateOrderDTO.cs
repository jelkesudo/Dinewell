using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class CreateOrderDTO
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string UserAddress { get; set; }
        public List<MealDTO> Meals { get; set; } = new List<MealDTO>();
    }

    public class MealDTO
    {
        public int MealId { get; set; }
        public List<SideDTO> Sides { get; set; } = new List<SideDTO>();
        public int Quantity { get; set; }
    }

    public class SideDTO
    {
        public int SideId { get; set; }
    }
}
