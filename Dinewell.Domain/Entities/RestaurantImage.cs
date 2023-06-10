using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class RestaurantImage : Entity
    {
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }
        public string ImageExtension { get; set; }
        public int ImageSize { get; set; }
        public byte[] ImageData { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
