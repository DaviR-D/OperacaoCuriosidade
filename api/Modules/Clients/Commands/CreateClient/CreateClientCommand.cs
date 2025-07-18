using Api.Modules.Clients.Commands.Interfaces;

namespace Api.Modules.Clients.Commands.CreateClient
{
    public class CreateClientCommand(ClientDto client) : IClientCommand
    {
        public string Name { get; set; } = client.Name;

    }
}
