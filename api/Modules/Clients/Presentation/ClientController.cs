using Api.Modules.Clients.Application;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.LockClient;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.GetClientsLength;
using Api.Modules.Clients.Application.Queries.GetLastMonthClients;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Application.Queries.GetPendingClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Modules.Clients.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientsHandlerFactory factory) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Create");
            var response = handler.Handle(new CreateClientCommand(client));
            if (response.Message == "email already in use")
                return Conflict(response);
            if (response.Message == "invalid data")
                return UnprocessableEntity(response);

            return Ok(response);
        }

        [HttpPost("lock/{clientId}")]
        public IActionResult Lock([FromRoute] Guid clientId)
        {
            var handler = factory.GetHandler("Lock");
            var response = handler.Handle(
                new LockClientCommand(
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                clientId: clientId
                )
            );
            if (response.Message == "client already locked")
                return Conflict(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("GetSingle");
            var response = handler.Handle(new GetSingleClientQuery(id));
            if (response.Message == "client does not exist")
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("length")]
        public IActionResult GetLength()
        {
            var handler = factory.GetHandler("GetLength");
            var response = handler.Handle(new GetClientsLengthQuery());
            return Ok(response);
        }

        [HttpGet("lastMonth")]
        public IActionResult GetLastMonth()
        {
            var handler = factory.GetHandler("GetLastMonth");
            var response = handler.Handle(new GetLastMonthClientsQuery());
            return Ok(response);
        }

        [HttpGet("pending")]
        public IActionResult GetPending()
        {
            var handler = factory.GetHandler("GetPending");
            var response = handler.Handle(new GetPendingClientsQuery());
            return Ok(response);
        }

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var handler = factory.GetHandler("GetPage");
            var response = handler.Handle(new GetPagedClientsQuery(start, increment));
            return Ok(response);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var handler = factory.GetHandler("GetSortedPage");
            var response = handler.Handle(new GetSortedClientsQuery(sortKey, descending, start, increment));
            return Ok(response);
        }

        [HttpGet("page/search")]
        public IActionResult SearchClients(int start, int increment, string query = "")
        {
            var handler = factory.GetHandler("SearchClients");
            var response = handler.Handle(new SearchClientsQuery(start, increment, query));
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Update");
            var response = handler.Handle(new UpdateClientCommand(client));
            if (response.Message == "invalid data")
                return UnprocessableEntity(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("Delete");
            var response = handler.Handle(new DeleteClientCommand(id));
            if (response.Message == "client does not exist")
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("checkEmail")]
        public IActionResult CheckEmail(string email, Guid? id = null)
        {
            var handler = factory.GetHandler("CheckEmail");
            var response = handler.Handle(new VerifyAvailableEmailQuery(id, email));
            return Ok(response);
        }
    }
}