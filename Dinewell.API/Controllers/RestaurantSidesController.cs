using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Implementation.Validators;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantSidesController : ControllerBase
    {
        // GET: api/<RestaurantSidesController>
        [HttpGet("admin")]
        public IActionResult Get([FromQuery] RestaurantSideSearch search, [FromServices] ISearchRestaurantSidesQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<RestaurantSidesController>/5
        [HttpGet("admin/{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificRestaurantSidesQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<RestaurantSidesController>
        [HttpPost("admin")]
        public IActionResult Post([FromBody] CreateRestaurantSideDTO dto, [FromServices] ICreateRestaurantSideCommand command, [FromServices] CreateRestaurantSidesValidator validator, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return NoContent();
        }

        // PUT api/<RestaurantSidesController>/5
        [HttpPut("admin/{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRestaurantSideDTO dto, [FromServices] IUpdateRestautantSideCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<RestaurantSidesController>/5
        [HttpDelete("admin/{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantSideCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
