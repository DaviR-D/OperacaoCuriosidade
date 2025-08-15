using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;
using System.Threading.Tasks;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var command = (UnlockClientCommand)input;
            var client = await repository.GetOneAsync(command.ClientId);

            var clientLock = client.EditLock?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var tokenLock = command.TokenExpireDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (clientLock != tokenLock)
                return new UnlockClientResponse(message: "invalid token");

            await repository.UnlockAsync(command.ClientId);

            return new UnlockClientResponse();
        }
    }
}
