using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Queries.GetPagedClients
{
    public class GetPagedClientsResponse(List<ClientPreviewDto> page) : IClientOutput
    {
        public List<ClientPreviewDto> Page { get; set; } = page;
    }
}
