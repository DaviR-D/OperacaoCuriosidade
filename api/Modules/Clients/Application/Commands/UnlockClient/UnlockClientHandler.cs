using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientHandler(ClientRepository repository, AuthenticationSettings authentication) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (UnlockClientCommand)input;
            var client = repository.GetOne(command.ClientId);

            if (client.Lock?.ToString("yyyy-MM-dd HH:mm:ss.fff") != command.TokenExpireDate.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                return new UnlockClientResponse(message: "invalid token");

            client.Lock = null;

            return new UnlockClientResponse();
        }
    }
}
