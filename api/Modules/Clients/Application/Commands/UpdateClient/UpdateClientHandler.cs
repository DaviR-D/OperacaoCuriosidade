using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        public IClientOutput Handle(IClientInput input)
        {
            var command = (UpdateClientCommand)input;
            repository.Update(command.Client);

            return new UpdateClientResponse();
        }
    }
}
