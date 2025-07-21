using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        public IClientOutput Handle(IClientInput input)
        {
            var command = (DeleteClientCommand)input;
            repository.Delete(command.Id);

            return new DeleteClientResponse();
        }
    }
}
