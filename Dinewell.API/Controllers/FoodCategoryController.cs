using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Implementation;
using Dinewell.Implementation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        // GET: api/<FoodCategoryController>
        [HttpGet("admin")]
        public IActionResult Get([FromQuery] FoodCategorySearch search, [FromServices] ISearchFoodCategoriesQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET: api/<FoodCategoryController>
        [HttpGet]
        public IActionResult Get([FromQuery] FoodCategorySearch search, [FromServices] IUserSearchFoodCategoriesQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        //GET api/<FoodCategoryController>/5
        [HttpGet("admin/{id}")]
        public IActionResult Get(int id, [FromServices] IQueryHandler handler, [FromServices] ISearchSpecificFoodCategoryQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        //GET api/<FoodCategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IQueryHandler handler, [FromServices] IUserSearchSpecificFoodCategoryQuery query)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<FoodCategoryController>
        [HttpPost("admin")]
        public IActionResult Post([FromBody] CreateFoodCategoryDTO dto, [FromServices] ICreateFoodCategoryCommand command, [FromServices] CreateFoodCategoryValidator validator, [FromServices] ICommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<FoodCategoryController>/5
        [HttpPut("admin/{id}")]
        public IActionResult Put(int id, [FromBody] UpdateFoodCategoryDTO dto, [FromServices] IUpdateFoodCategoryCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command,dto);
            return StatusCode(204);
        }
    }
}
