using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientCommand(ClientDto client) : IClientInput
    {
        public ClientDto Client { get; set; } = client;

    }
}
