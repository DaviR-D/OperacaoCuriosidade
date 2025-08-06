using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        private static readonly Lock _lock = new();

        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (CreateClientCommand)input;
            ClientValidator validator = new(command.Client);

            if (!validator.ValidateClient())
                return new CreateClientResponse(message: "invalid data");

            Guid? newId;
            lock (_lock)
            {
                if (!VerifyAvailableEmail(command.Client.Email))
                    return new CreateClientResponse(message: "email already in use");

                newId = repository.Create(command.Client);
            }

            logService.Create(userId: command.UserId, clientId: (Guid)newId, action: "Create");
            return new CreateClientResponse(id: newId);
        }
        public bool VerifyAvailableEmail(string email)
        {
            Client? existingEmail = repository.GetAll().FirstOrDefault(command => command.Email == email);
            return existingEmail == null;
        }
    }
}
