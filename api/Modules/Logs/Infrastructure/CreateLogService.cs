using Api.Modules.Logs.Infrastructure.Repositories;

namespace Api.Modules.Logs.Infrastructure
{
    public class CreateLogService(LogRepository repository)
    {
        public void Create(Guid userId, Guid clientId, string action)
        {
            var id = Guid.NewGuid();
            var timeStamp = DateTime.Now;

            repository.Create(new(
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
