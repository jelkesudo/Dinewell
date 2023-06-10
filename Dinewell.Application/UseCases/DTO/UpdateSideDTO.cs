using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class UpdateSideDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
