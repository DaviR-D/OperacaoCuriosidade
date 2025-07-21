using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientResponse(Client client) : IClientOutput
    {
        public Client Client { get; set; } = client;
    }
}
