using Api.Modules.Logs.Application;
using Api.Modules.Logs.Application.Queries.GetLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Logs.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogController(LogHandlerFactory factory) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(int start, int increment)
        {
            var handler = factory.GetHandler("GetAll");
            var response = handler.Handle(new GetLogsQuery(start: start, increment: increment));

            return Ok(response);
        }
    }
}
