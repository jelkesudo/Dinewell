using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderAddress { get; set; }
        public IEnumerable<OrderMealsDTO> Meals { get; set; } = new List<OrderMealsDTO>();
        public decimal Total { get; set; }
    }
    public class OrderMealsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<OrderSidesDTO> Sides { get; set; } = new List<OrderSidesDTO>();
    }
    public class OrderSidesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
