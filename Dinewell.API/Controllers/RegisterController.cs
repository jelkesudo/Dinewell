using Dinewell.API.Extensions;
using Dinewell.Application.UseCaseHandling;
using Dinewell.Application.UseCases.Commands;
using Dinewell.Application.UseCases.DTO;
using Dinewell.Implementation;
using Dinewell.Implementation.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dinewell.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private CommandHandler _handler;
        private ICreateUserCommand _command;
        public RegisterController(ICreateUserCommand command, CommandHandler handler)
        {
            _command = command;
            _handler = handler;
        }
        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDTO dto, [FromServices] CreateUserValidator validator)
        {
            _handler.HandleCommand(_command, dto);
            return StatusCode(201);
        }
    }
}
