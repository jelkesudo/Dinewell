using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class RestaurantMenuImage : Entity
    {
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }
        public string ImageExtension { get; set; }
        public int ImageSize { get; set; }
        public byte[] ImageData { get; set; }
        public int RestaurantMenuId { get; set; }
        public virtual RestaurantMenu RestaurantMenu { get; set; }
    }
}
