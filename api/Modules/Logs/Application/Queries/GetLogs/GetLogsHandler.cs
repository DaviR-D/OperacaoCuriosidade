using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsHandler(LogRepository logRepository, ClientRepository clients, UserRepository users) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (GetLogsQuery)input;
            var logs = logRepository.GetAll();
            var logsPage = logs
                .Skip(query.Start)
                .Take(query.Increment)
                .Select(log => LogDtoMapper
                .ToResponseDto(log, clients, users))
                .ToList();
            return new GetLogsResponse(logs: logsPage, logsLength: logs.Count);
        }
    }
}
