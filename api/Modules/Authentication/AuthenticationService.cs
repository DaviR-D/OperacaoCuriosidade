using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace Api.Modules.Authentication
{
    public class AuthenticationService(List<User> users)
    {
        public string Authenticate(UserDto user)
        {
            foreach (var u in users)
            {
                if(user.Email == u.Email)
                {
                    if(user.Password == u.Password)
                    {
                        return GenerateToken(u);
                    }
                }
            }
            return null;
        }
        private string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(AuthenticationSettings.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Name));

            return ci;
        }
    }
}
