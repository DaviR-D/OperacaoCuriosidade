using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (UnlockClientCommand)input;
            var client = repository.GetOne(command.ClientId);

            var clientLock = client.Lock?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var tokenLock = command.TokenExpireDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            if (clientLock != tokenLock)
                return new UnlockClientResponse(message: "invalid token");

            client.Lock = null;

            return new UnlockClientResponse();
        }
    }
}
