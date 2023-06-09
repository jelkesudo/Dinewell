using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Queries.Searches;
using Dinewell.Application.UseCases.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Implementation.Validators;
using Dinewell.Application.UseCases.Commands;
using System.Linq;
using System.Security.Claims;
using Dinewell.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearch search, [FromServices] ISearchOrdersQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] ISearchSpecificOrdersQuery query, [FromServices] IQueryHandler handler)
        {
            return Ok(handler.HandleQuery(query, id));
        }

        // POST api/<OrderController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateOrderDTO dto, [FromServices] ICreateOrderCommand command, [FromServices] CreateOrderValidator validator, [FromServices] CommandHandler handler)
        {
            Claim userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return Forbid();
            }
            dto.UserId = int.Parse(userIdClaim.Value);
            handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
