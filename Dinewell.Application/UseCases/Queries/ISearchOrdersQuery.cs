using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries
{
    public interface ISearchOrdersQuery : IQuery<OrderSearch, PagedResponse<OrderDTO>>
    {
    }
}
