using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;
using System.Threading.Tasks;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var command = (UpdateClientCommand)input;
            var client = await repository.GetOneAsync(command.ClientId);

            var clientLock = client.EditLock?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var tokenLock = command.TokenExpireDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (clientLock != tokenLock)
                return new UpdateClientResponse(message: "invalid token");

            command.Client.Id = command.ClientId;
            ClientValidator validator = new(command.Client);

            if (!validator.ValidateClient())
                return new UpdateClientResponse(message: "invalid data");

            await repository.UpdateAsync(command.Client);

            logService.Create(userId: command.UserId, clientId: command.ClientId, action: "Update");
            return new UpdateClientResponse();
        }
    }
}
