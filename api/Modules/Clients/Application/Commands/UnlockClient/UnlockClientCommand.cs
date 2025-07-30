using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientCommand(Guid clientId, Guid userId) : IRequestInput
    {
        public Guid ClientId { get; set; } = clientId;
        public Guid UserId { get; set; } = userId;
    }
}
