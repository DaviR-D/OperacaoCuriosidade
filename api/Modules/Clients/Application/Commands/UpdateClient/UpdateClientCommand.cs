using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientCommand(ClientDto client) : IClientInput
    {
        public ClientDto Client { get; set; } = client;
    }
}
