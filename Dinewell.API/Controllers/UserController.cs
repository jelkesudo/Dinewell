using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] ISearchUsersQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserDto dto, [FromServices] IUpdateUserCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
