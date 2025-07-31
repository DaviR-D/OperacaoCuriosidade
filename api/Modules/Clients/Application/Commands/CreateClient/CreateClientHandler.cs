using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        private static readonly Lock _lock = new();

        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (CreateClientCommand)input;
            ClientValidator validator = new(command.Client);

            if (!validator.ValidateClient())
                return new CreateClientResponse(message: "invalid data");

            Guid? newId = new();
            lock (_lock)
            {
                if (VerifyAvailableEmail(command.Client.Email))
                    newId = repository.Create(command.Client);
                else
                    return new CreateClientResponse(message: "email already in use");
            }
            return new CreateClientResponse(id: newId);
        }
        public bool VerifyAvailableEmail(string email)
        {
            Client? existingEmail = repository.GetAll().FirstOrDefault(command => command.Email == email);
            return existingEmail == null;
        }
    }
}
