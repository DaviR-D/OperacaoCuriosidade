using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientHandler(ClientRepository repository, AuthenticationSettings authentication) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (UnlockClientCommand)input;
            var client = repository.GetOne(command.ClientId);
            client.Lock = null;

            return new UnlockClientResponse();
        }
    }
}
