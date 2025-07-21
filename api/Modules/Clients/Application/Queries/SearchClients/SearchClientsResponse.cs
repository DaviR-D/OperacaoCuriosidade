using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsResponse(List<ClientPreviewDto> results) : IRequestOutput
    {
        public List<ClientPreviewDto> Results { get; set; } = results;
    }
}
