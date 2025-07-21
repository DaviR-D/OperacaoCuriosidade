using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.SearchClients;

namespace Api.Modules.Clients.Presentation
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientRepository repository) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ClientDto client)
        {
            var handler = new CreateClientHandler(repository);
            handler.Handle(new CreateClientCommand(client));
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            var handler = new GetSingleClientHandler(repository);
            var response = handler.Handle(new GetSingleClientQuery(id));
            var client = (GetSingleClientResponse)response;
            return Ok(client.Client);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var handler = new GetClientsStatsHandler(repository);
            return Ok(handler.Handle(new GetClientsStatsQuery()));
        }

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var handler = new GetPagedClientsHandler(repository);
            var response = handler.Handle(new GetPagedClientsQuery(start, increment));
            var page = (GetPagedClientsResponse)response;
            return Ok(page.Page);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var handler = new GetSortedClientsHandler(repository);
            var response = handler.Handle(new GetSortedClientsQuery(sortKey, descending, start, increment));
            var page = (GetSortedClientsResponse)response; 
            return Ok(page.Page);
        }

        [HttpGet("page/search")]
        public IActionResult SearchClients(int start, int increment, string query = "")
        {
            var handler = new SearchClientsHandler(repository);
            var response = handler.Handle(new SearchClientsQuery(start, increment, query));
            var results = (SearchClientsResponse)response;

            return Ok(results.Results);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDto client)
        {
            var handler = new UpdateClientHandler(repository);
            handler.Handle(new UpdateClientCommand(client));
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var handler = new DeleteClientHandler(repository);
            handler.Handle(new DeleteClientCommand(id));
            return Ok();
        }

        [HttpGet("checkEmail")]
        public IActionResult CheckEmail(Guid id, string email)
        {
            var handler = new VerifyAvailableEmailHandler(repository);
            var response = handler.Handle(new VerifyAvailableEmailQuery(id, email));
            var available = (VerifyAvailableEmailResponse)response;
            return Ok(available.IsAvailable);
        }
    }
}