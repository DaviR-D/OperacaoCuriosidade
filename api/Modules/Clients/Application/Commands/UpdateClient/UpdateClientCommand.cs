using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientCommand(ClientDto client, Guid clientId, DateTime tokenExpireDate, Guid userId) : IRequestInput
    {
        public ClientDto Client { get; set; } = client;
        public Guid ClientId { get; set; } = clientId;
        public DateTime TokenExpireDate { get; set; } = tokenExpireDate;
        public Guid UserId { get; set; } = userId;
    }
}
