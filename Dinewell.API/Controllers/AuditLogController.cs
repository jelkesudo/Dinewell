using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        // GET: api/<AuditLogController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchAuditLog search, [FromServices] ISearchAuditLogQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }
    }
}
