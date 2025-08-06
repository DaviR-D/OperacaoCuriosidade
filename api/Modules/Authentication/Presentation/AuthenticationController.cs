using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authentication.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationHandlerFactory factory, RequestResponseFactory responseFactory) : ControllerBase
    {
        [HttpPost("signup")]
        public IActionResult Create([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Signup");
            var response = handler.Handle(new CreateUserCommand(user));

            return responseFactory.GetResponse(response);
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Authenticate");
            var response = handler.Handle(new AuthenticateCommand(user));

            return responseFactory.GetResponse(response);
        }
    }
}
