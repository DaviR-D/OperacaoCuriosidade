using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsResponse(List<ClientPreviewDto> results) : IClientOutput
    {
        public List<ClientPreviewDto> Results { get; set; } = results;
    }
}
