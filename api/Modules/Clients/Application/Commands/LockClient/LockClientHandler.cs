using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Modules.Clients.Application.Commands.LockClient
{
    public class LockClientHandler(ClientRepository repository, AuthenticationSettings authentication) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (LockClientCommand)input;
            var client = repository.GetOne(command.ClientId);

            if (client.Lock != null && client.Lock > DateTime.UtcNow)
                return new LockClientResponse(message:"client already locked");

            var expireTime = DateTime.UtcNow.AddSeconds(15);

            client.Lock = expireTime;
            var token = GenerateToken(command.UserId, command.ClientId, expireTime);

            return new LockClientResponse(token: token);
        }
        private string GenerateToken(Guid userId, Guid clientId, DateTime expireTime)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(authentication.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(userId, clientId),
                SigningCredentials = credentials,
                Expires = expireTime,
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(Guid userId, Guid clientId)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.NameIdentifier, value: userId.ToString()));
            claimsIdentity.AddClaim(new Claim(type: "ClientId", value: clientId.ToString()));

            return claimsIdentity;
        }
    }
}
