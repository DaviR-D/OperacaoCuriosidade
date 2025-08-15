using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Modules.Clients.Application.Commands.LockClient
{
    public class LockClientHandler(ClientRepository repository, AuthenticationSettings authentication) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        private static readonly Lock _lock = new();
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var command = (LockClientCommand)input;
            var client = await repository.GetOneAsync(command.ClientId);

            if (client == null || client.Deleted == true)
            {
                return new LockClientResponse(message: "client does not exist");
            }

            lock (_lock)
            {
                if (client.EditLock != null && client.EditLock > DateTime.UtcNow)
                    return new LockClientResponse(message: "client already locked");

                var expireTime = DateTime.UtcNow.AddSeconds(60);

                repository.LockAsync(command.ClientId, expireTime);

                var token = GenerateToken(command.UserId, command.ClientId, expireTime);
                return new LockClientResponse(token: token);
            }
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
                Subject = GenerateClaims(userId, clientId, expireTime),
                SigningCredentials = credentials,
                Expires = expireTime,
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(Guid userId, Guid clientId, DateTime expireTime)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(
                type: ClaimTypes.NameIdentifier,
                value: userId.ToString())
                );

            claimsIdentity.AddClaim(new Claim(
                type: ClaimTypes.Expiration,
                value: expireTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                );

            claimsIdentity.AddClaim(new Claim(
                type: "ClientId",
                value: clientId.ToString())
                );

            return claimsIdentity;
        }
    }
}
