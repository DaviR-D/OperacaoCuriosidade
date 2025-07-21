using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientCommand(Guid id) : IClientInput
    {
        public Guid Id { get; set; } = id;
    }
}
