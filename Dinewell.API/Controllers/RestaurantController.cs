using Dinewell.API.Extensions;
using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Implementation;
using Dinewell.Implementation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        [HttpGet("admin")]
        [Authorize]
        public IActionResult Get([FromQuery] RestaurantSearch search, [FromServices] ISearchRestaurantsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] RestaurantSearch search, [FromServices] IUserSearchRestaurantsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<RestaurantController>/5
        [HttpGet("admin/{id}")]
        [Authorize]
        public IActionResult Get(int id, [FromServices] ISearchSpecificRestaurantsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IUserSearchSpecificRestaurantsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        [HttpPost("admin")]
        [Authorize]
        public IActionResult Post([FromBody] CreateRestaurantDTO dto, [FromServices] ICreateRestaurantCommand command, [FromServices] CreateRestaurantValidator validator, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return NoContent();
        }

        // PUT api/<RestaurantController>/5
        [HttpPut("admin/{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRestaurantDTO dto, [FromServices] IUpdateRestaurantCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<RestaurantController>/5
        [HttpDelete("admin/{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
