using Dinewell.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.DTO
{
    public class SearchAuditLog : PagedSearch
    {
        public string Username { get; set; }
        public string UseCaseName { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
