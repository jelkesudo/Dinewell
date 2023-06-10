using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class UserOrderSearch : PagedSearch
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
