using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Modules.Authentication.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationHandlerFactory factory, RequestResponseFactory responseFactory) : ControllerBase
    {
        [HttpPost("signup")]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Signup");
            var response = await handler.HandleAsync(new CreateUserCommand(user));

            return responseFactory.GetResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserDto user)
        {
            var handler = factory.GetHandler("Authenticate");
            var response = await handler.HandleAsync(new AuthenticateCommand(user));

            return responseFactory.GetResponse(response);
        }
    }
}
