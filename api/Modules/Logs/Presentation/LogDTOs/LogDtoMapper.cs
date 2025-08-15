using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Domain;
using System.Threading.Tasks;

namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class LogDtoMapper
    {
        public static async Task<LogDto> ToDto(Log log, ClientRepository clients, UserRepository users)
        {
            var user = await users.GetOne(log.UserId);
            var client = await clients.GetOneAsync(log.ClientId);

            return new LogDto(
                userEmail: user == null ? "unknown" : user.Email,
                clientEmail: client == null ? "unknown" : client.Email,
                timeStamp: log.TimeStamp,
                action: log.Action
                );
        }
    }
}
