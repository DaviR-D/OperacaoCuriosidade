using Api.Modules.Repositories;

namespace Api.Modules.Clients.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository) : IClientHandler<CreateClientResponse, CreateClientCommand>
    {
        private static readonly Lock _lock = new();

        public CreateClientResponse Handle(CreateClientCommand command)
        {
            lock (_lock)
            {
                command.Id = Guid.NewGuid();
                command.Date = DateTime.Now;
                if (VerifyAvailableEmail(command.Id, command.Email)) repository.Create(command);
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
