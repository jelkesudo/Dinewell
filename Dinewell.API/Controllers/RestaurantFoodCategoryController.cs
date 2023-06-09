using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Dinewell.Implementation.Validators;
using Dinewell.Implementation.UseCases.Commands;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantFoodCategoryController : ControllerBase
    {
        // POST api/<RestaurantFoodCategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateRestaurantFoodCategoryDTO dto, [FromServices] ICreateRestaurantFoodCategoryCommand command, [FromServices] CreateRestaurantFoodCategoriesValidator validator, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // DELETE api/<RestaurantFoodCategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRestaurantFoodCategoryCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
