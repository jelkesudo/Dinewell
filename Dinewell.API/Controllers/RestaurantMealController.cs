using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Implementation.Validators;
using Dinewell.Application.UseCases.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantMealController : ControllerBase
    {
        // GET: api/<RestaurantMealController>
        [HttpGet]
        public IActionResult Get([FromQuery] MealSearch search, [FromServices] ISearchMealsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }
        // GET api/<RestaurantMealController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificMealQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<RestaurantMealController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateMealDTO dto, [FromServices] ICreateRestaurantMenuCommand command, [FromServices] CreateRestaurantMenuValidator validator, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<RestaurantMealController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRestaurantMealDTO dto, [FromServices] IUpdateRestaurantMenuCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<RestaurantMealController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantMealCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
