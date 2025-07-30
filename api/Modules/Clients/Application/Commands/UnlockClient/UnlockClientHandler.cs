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
        private string GenerateToken(Guid userId)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(authentication.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(userId),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddSeconds(15),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(Guid userId)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.NameIdentifier, value: userId.ToString()));

            return claimsIdentity;
        }
    }
}
