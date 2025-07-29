using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.LockClient
{
    public class LockClientCommand(Guid userId, Guid clientId) : IRequestInput
    {
        public Guid UserId { get; set; } = userId;
        public Guid ClientId { get; set; } = clientId;
    }
}
