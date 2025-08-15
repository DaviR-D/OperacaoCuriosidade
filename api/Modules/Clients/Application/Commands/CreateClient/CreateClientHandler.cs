using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var command = (CreateClientCommand)input;
            ClientValidator validator = new(command.Client);

            if (!validator.ValidateClient())
                return new CreateClientResponse(message: "invalid data");

            Guid? newId;

            if (!(await VerifyAvailableEmail(command.Client.Email)))
                return new CreateClientResponse(message: "email already in use");

            newId = await repository.CreateAsync(command.Client);


            logService.Create(userId: command.UserId, clientId: (Guid)newId, action: "Create");
            return new CreateClientResponse(id: newId);
        }
        public async Task<bool> VerifyAvailableEmail(string email)
        {
            var existingEmailTask = await repository.GetAllAsync();

            var existingEmail = existingEmailTask.FirstOrDefault(command => command.Email == email);

            return existingEmail == null;
        }
    }
}
