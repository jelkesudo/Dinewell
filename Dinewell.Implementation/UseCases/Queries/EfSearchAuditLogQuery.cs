using Dinewell.Application.Exceptions;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Implementation.UseCases.Queries
{
    public class EfSearchAuditLogQuery : UseCases.EfUseCase, ISearchAuditLogQuery
    {
        public EfSearchAuditLogQuery(DinewellContext context) : base(context)
        {
        }

        public int Id => 44;

        public string Name => "Search audit log (EF)";

        public string Description => "";

        public PagedResponse<AuditLogDTO> Execute(SearchAuditLog search)
        {
            var query = Context.LogEntries.AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Actor.ToLower().Contains(search.Username.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.DateFrom))
            {
                DateTime searchDateFrom;
                if (!DateTime.TryParse(search.DateFrom, out searchDateFrom))
                {
                    throw new FormatException("Invalid date from format");
                }
                searchDateFrom = DateTime.Parse(search.DateFrom);
                query = query.Where(x => x.CreatedAt > searchDateFrom);
            }

            if (!string.IsNullOrEmpty(search.DateTo))
            {
                DateTime searchDateTo;
                if (!DateTime.TryParse(search.DateTo, out searchDateTo))
                {
                    throw new FormatException("Invalid date to format");
                }
                searchDateTo = DateTime.Parse(search.DateTo);
                query = query.Where(x => x.CreatedAt > searchDateTo);
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<AuditLogDTO>();

            response.TotalCount = query.Count();

            response.Data = query.Select(x => new AuditLogDTO
            {
                Id = x.Id,
                Actor = x.Actor,
                UseCaseName = x.UseCaseName,
                UseCaseData = x.UseCaseData,
                CreatedAt = x.CreatedAt,
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;

        }
    }
}
