using Dinewell.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Application.UseCases.Queries
{
    public interface IUserSearchSpecificFoodCategoryQuery : IQuery<int, SingleFoodCategoriyDTO>
    {
    }
}
