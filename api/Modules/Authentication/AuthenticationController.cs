using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationService service) : ControllerBase
    {
        [HttpPost]
        public ActionResult Authenticate([FromBody] UserDto user)
        {
            var token = service.Authenticate(user);
            if(token != null) return Ok(token);
            else return Unauthorized();
        }
    }
}
