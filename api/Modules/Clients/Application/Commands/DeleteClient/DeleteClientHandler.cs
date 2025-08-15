using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;
using System.Threading.Tasks;

namespace Api.Modules.Clients.Application.Commands.DeleteClient
{
    public class DeleteClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var command = (DeleteClientCommand)input;
            var client = await repository.GetOneAsync(command.Id);

            if (client != null && client.EditLock != null)
                return new DeleteClientResponse(message: "client is locked");

            var alreadyDeleted = await repository.Delete(command.Id);

            if (alreadyDeleted)
                return new DeleteClientResponse(message: "client does not exist");

            logService.Create(userId: command.UserId, clientId: command.Id, action: "Delete");
            return new DeleteClientResponse();
        }
    }
}
