using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientCommand(ClientDto client, Guid clientId, DateTime tokenExpireDate) : IRequestInput
    {
        public ClientDto Client { get; set; } = client;
        public Guid ClientId { get; set; } = clientId;
        public DateTime TokenExpireDate { get; set; } = tokenExpireDate;
    }
}
