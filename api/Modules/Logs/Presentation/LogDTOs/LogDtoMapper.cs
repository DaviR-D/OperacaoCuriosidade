using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Domain;

namespace Api.Modules.Logs.Presentation.LogDTOs
{
    public class LogDtoMapper
    {
        public static Log ToEntity(CreateLogDto log)
        {
            return new Log(
                id: (Guid)log.Id,
                userId: (Guid)log.UserId,
                clientId: log.ClientId,
                timeStamp: (DateTime)log.TimeStamp,
                action: log.Action
                );
        }
        public static CreateLogDto ToDto(Log log)
        {
            return new CreateLogDto(
                id: log.Id,
                userId: log.UserId,
                clientId: log.ClientId,
                timeStamp: log.TimeStamp,
                action: log.Action
                );
        }
        public static GetLogDto ToResponseDto(Log log, ClientRepository clients, UserRepository users)
        {
            var user = users.GetOne(log.UserId);
            var client = clients.GetOne(log.ClientId);

            return new GetLogDto(
                userEmail: user == null ? "unknown" : user.Email,
                clientEmail: client == null ? "unknown" : client.Email,
                timeStamp: log.TimeStamp,
                action: log.Action
                );
        }
    }
}
