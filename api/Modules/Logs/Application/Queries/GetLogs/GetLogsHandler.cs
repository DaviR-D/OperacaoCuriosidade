using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsHandler(LogRepository logRepository, ClientRepository clients, UserRepository users) : IRequestHandler<Task<IRequestOutput?>, IRequestInput>
    {
        public async Task<IRequestOutput?> HandleAsync(IRequestInput input)
        {
            var query = (GetLogsQuery)input;
            var logs = logRepository.GetAll();
            var logsPageTask = logs
                .Skip(query.Start)
                .Take(query.Increment)
                .Select(async log => await LogDtoMapper
                .ToDto(log, clients, users));

            var logsPage = await Task.WhenAll(logsPageTask);

            return new GetLogsResponse(logs: [.. logsPage], logsLength: logs.Count);
        }
    }
}
