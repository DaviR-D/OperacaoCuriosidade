using Api.Modules.Authentication.Domain;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientCommand(Guid id, Guid userId) : IRequestInput
    {
        public Guid Id { get; set; } = id;
        public Guid UserId { get; set; } = userId;
    }
}
