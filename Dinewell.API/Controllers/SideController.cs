using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SideController : ControllerBase
    {
        // GET: api/<SideController>
        [HttpGet("admin")]
        public IActionResult Get([FromQuery] SideSearch search, [FromServices] ISearchSidesQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<SideController>/5
        [HttpGet("admin/{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificSideQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<SideController>
        [HttpPost("admin")]
        public IActionResult Post([FromBody] CreateSideDto dto, [FromServices] ICreateSidesCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<SideController>/5
        [HttpPut("admin/{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSideDTO dto, [FromServices] IUpdateSideCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
