using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Domain;

namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class LogDtoMapper
    {
        public static LogDto ToDto(Log log, ClientRepository clients, UserRepository users)
        {
            var user = users.GetOne(log.UserId);
            var client = clients.GetOne(log.ClientId);

            return new LogDto(
                userEmail: user == null ? "unknown" : user.Email,
                clientEmail: client == null ? "unknown" : client.Email,
                timeStamp: log.TimeStamp,
                action: log.Action
                );
        }
    }
}
