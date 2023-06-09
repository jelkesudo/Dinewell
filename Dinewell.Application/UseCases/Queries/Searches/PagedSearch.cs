using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries.Searches
{
    public class PagedSearch
    {
        public int? PerPage { get; set; } = 15;
        public int? Page { get; set; } = 1;
    }
}
