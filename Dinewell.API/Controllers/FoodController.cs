using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        // GET: api/<FoodController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchFoodDTO search, [FromServices] ISearchFoodsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<FoodController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificFoodsQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<FoodController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateFoodDTO dto, [FromServices] ICreateFoodCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return NoContent();
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateFoodDTO dto, [FromServices] IUpdateFoodCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
