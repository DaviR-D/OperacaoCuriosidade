using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientResponse(ClientDto? client = null, string? message = null) : IRequestOutput
    {
        public ClientDto? Client { get; set; } = client;
        public string? Message { get; set; } = message;
    }
}
