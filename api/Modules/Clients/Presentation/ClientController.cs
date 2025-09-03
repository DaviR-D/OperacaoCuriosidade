using Api.Modules.Clients.Application;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.LockClient;
using Api.Modules.Clients.Application.Commands.UnlockClient;
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
using Api.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Modules.Clients.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientsHandlerFactory factory, RequestResponseFactory responseFactory) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Create");
            var response = await handler.HandleAsync(new CreateClientCommand(
                client: client,
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                )
             );

            return responseFactory.GetResponse(response);
        }

        [HttpPost("lock/{clientId}")]
        public async Task<IActionResult> Lock([FromRoute] Guid clientId)
        {
            var handler = factory.GetHandler("Lock");
            var response = await handler.HandleAsync(
                new LockClientCommand(
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                clientId: clientId
                )
            );

            return responseFactory.GetResponse(response);
        }

        [HttpPost("unlock")]
        public async Task<IActionResult> Unlock()
        {
            var handler = factory.GetHandler("Unlock");
            var response = await handler.HandleAsync(
                new UnlockClientCommand(
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                clientId: Guid.Parse(User.FindFirst("ClientId")?.Value),
                tokenExpireDate: DateTime.Parse(User.FindFirst(ClaimTypes.Expiration)?.Value)
                )
            );

            return responseFactory.GetResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("GetSingle");
            var response = await handler.HandleAsync(new GetSingleClientQuery(
                id: id,
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                )
             );

            return responseFactory.GetResponse(response);
        }

        [HttpGet("length")]
        public async Task<IActionResult> GetLength()
        {
            var handler = factory.GetHandler("GetLength");
            var response = await handler.HandleAsync(new GetClientsLengthQuery());

            return responseFactory.GetResponse(response);
        }

        [HttpGet("lastMonth")]
        public async Task<IActionResult> GetLastMonth()
        {
            var handler = factory.GetHandler("GetLastMonth");
            var response = await handler.HandleAsync(new GetLastMonthClientsQuery());

            return responseFactory.GetResponse(response);
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var handler = factory.GetHandler("GetPending");
            var response = await handler.HandleAsync(new GetPendingClientsQuery());

            return responseFactory.GetResponse(response);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPage(int start, int increment)
        {
            var handler = factory.GetHandler("GetPage");
            var response = await handler.HandleAsync(new GetPagedClientsQuery(start, increment));

            return responseFactory.GetResponse(response);
        }

        [HttpGet("page/sorted")]
        public async Task<IActionResult> GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var handler = factory.GetHandler("GetSortedPage");
            var response = await handler.HandleAsync(new GetSortedClientsQuery(sortKey, descending, start, increment));

            return responseFactory.GetResponse(response);
        }

        [HttpGet("page/search")]
        public async Task<IActionResult> SearchClients(int start, int increment, string query = "")
        {
            var handler = factory.GetHandler("SearchClients");
            var response = await handler.HandleAsync(new SearchClientsQuery(start, increment, query));

            return responseFactory.GetResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientDto client)
        {
            var handler = factory.GetHandler("Update");
            var response = await handler.HandleAsync(new UpdateClientCommand(
                client: client,
                clientId: Guid.Parse(User.FindFirst("ClientId")?.Value),
                tokenExpireDate: DateTime.Parse(User.FindFirst(ClaimTypes.Expiration)?.Value),
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                )
             );

            return responseFactory.GetResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var handler = factory.GetHandler("Delete");
            var response = await handler.HandleAsync(new DeleteClientCommand(
                id: id,
                userId: Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                )
             );

            return responseFactory.GetResponse(response);
        }

        [HttpGet("checkEmail")]
        public async Task<IActionResult> CheckEmail(string email, Guid? id = null)
        {
            var handler = factory.GetHandler("CheckEmail");
            var response = await handler.HandleAsync(new VerifyAvailableEmailQuery(id, email));

            return responseFactory.GetResponse(response);
        }
    }
}