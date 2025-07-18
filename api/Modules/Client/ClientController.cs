using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Clients
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(IClientService service, List<Client> clients) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] ClientDto client)
        {
            service.CreateClient(client);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] Guid id)
        {
            var client = service.GetSingleClient(id);
            return Ok(client);
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(service.GetClientsStats());
        }

        [HttpGet("page")]
        public IActionResult GetPage(int start, int increment)
        {
            var page = service.GetPagedClients(start, increment, clients);
            return Ok(page);
        }

        [HttpGet("page/sorted")]
        public IActionResult GetSortedPage(string sortKey, bool descending, int start, int increment)
        {
            var page = service.GetSortedClients(sortKey, descending, start, increment);
            return Ok(page);
        }

        [HttpGet("page/search")]
        public IActionResult SearchClients(int start, int increment, string query = "")
        {
            var results = service.SearchClients(query, start, increment);
            return Ok(results);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDto client)
        {
            service.UpdateClient(client);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            service.DeleteClient(id);
            return Ok();
        }

        [HttpGet("checkEmail")]
        public IActionResult CheckEmail(Guid id, string email)
        {
            var emailAvailable = service.VerifyAvailableEmail(id, email);
            return Ok(emailAvailable);
        }
    }
}