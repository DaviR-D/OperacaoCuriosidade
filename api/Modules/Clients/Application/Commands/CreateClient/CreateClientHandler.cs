using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        private static readonly Lock _lock = new();

        public IClientOutput Handle(IClientInput input)
        {
            var command = (CreateClientCommand)input;
            lock (_lock)
            {
                if (VerifyAvailableEmail(command.Client.Id, command.Client.Email))
                    repository.Create(command.Client);
            }
            return new CreateClientResponse();
        }
        public bool VerifyAvailableEmail(Guid id, string email)
        {
            Client? existingEmail = repository.GetAll().FirstOrDefault(command => command.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }
    }
}
