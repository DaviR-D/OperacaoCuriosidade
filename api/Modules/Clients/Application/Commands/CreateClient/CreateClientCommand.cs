using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientCommand(ClientDto client, Guid userId) : IRequestInput
    {
        public ClientDto Client { get; set; } = client;
        public Guid UserId { get; set; } = userId;

    }
}
