using Api.Modules.Logs.Infrastructure.Repositories;

namespace Api.Modules.Logs.Infrastructure
{
    public class CreateLogService(LogRepository repository)
    {
        public async Task Create(Guid userId, Guid clientId, string action)
        {
            var id = Guid.NewGuid();
            var timeStamp = DateTime.Now;

            await repository.Create(new(
                id: id,
                userId: userId,
                clientId: clientId,
                timeStamp: timeStamp,
                action: action
                )
             );
        }
    }
}
