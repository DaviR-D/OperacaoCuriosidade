using Api.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Shared
{
    public class RequestResponseFactory : ControllerBase
    {
        public IActionResult GetResponse(IRequestOutput response)
        {
            string message = response.Message ?? string.Empty;

            Dictionary<string, Func<IRequestOutput, IActionResult>> responses = new()
            {
                {"email already in use", Conflict },
                {"invalid data", UnprocessableEntity },
                {"client does not exist", NotFound},
                {"client already locked", Conflict },
                {"client is locked", Unauthorized },
                {"invalid token", Unauthorized },
                {"incorrect password", Unauthorized },
                {"incorrect email", Unauthorized },
                {string.Empty, Ok },
            };

            return responses[message](response);
        }
    }
}
