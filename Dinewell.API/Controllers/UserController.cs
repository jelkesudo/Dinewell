using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Application.UseCases.Queries;
using Dinewell.Application.UseCases.Queries.Searches;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet("admin")]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] ISearchUsersQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<UserController>/5
        [HttpGet("admin/{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificUsersQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // GET api/<UserController>/5
        [HttpGet("myorders")]
        public IActionResult Get([FromBody] UserOrderSearch search, [FromServices] IUserSearchHisOrdersQuery query, [FromServices] IQueryHandler handler)
        {
            Claim userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return Forbid();
            }
            var s = int.TryParse(userIdClaim.Value, out int num);
            if (!s)
            {
                return Forbid();
            }
            int check = int.Parse(userIdClaim.Value);
            search.Id = check;
            return Ok(handler.HandleQuery(query, search));
        }

        // PUT api/<UserController>/5
        [HttpPut("admin/{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserDto dto, [FromServices] IUpdateUserCommand command, [FromServices] CommandHandler handler)
        {
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserDto dto, [FromServices] IUserUpdateUserCommand command, [FromServices] CommandHandler handler)
        {
            Claim userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return Forbid();
            }
            var s = int.TryParse(userIdClaim.Value, out int num);
            if (!s)
            {
                return Forbid();
            }
            int check = int.Parse(userIdClaim.Value);
            if (check != id)
            {
                return Forbid();
            }
            dto.Id = id;
            handler.HandleCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("admin/{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand command, [FromServices] CommandHandler handler)
        {
            handler.HandleCommand(command, id);
            return StatusCode(204);
        }
    }
}
