using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authentication.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(UserRepository repository, AuthenticationSettings auth) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult Create([FromBody] UserDto user)
        {
            var handler = new CreateUserHandler(repository);
            handler.Handle(new CreateUserCommand(user));
            return Ok();
        }

        [HttpPost]
        public ActionResult Authenticate([FromBody] UserDto user)
        {
            var handler = new AuthenticateHandler(repository, auth);
            var response = handler.Handle(new AuthenticateCommand(user));
            if (response != null)
            {
                var authentication = response as AuthenticateResponse;
                var token = authentication.Token;
                return Ok(new { Token = token });
            }
            else return Unauthorized();
        }
    }
}
