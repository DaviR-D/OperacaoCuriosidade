using Api.Modules.Logs.Application.Queries.GetLogs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application
{
    public class LogHandlerFactory(IServiceProvider service)
    {
        private readonly Dictionary<string, Type> Handlers = new()
        {
            {"GetAll", typeof(GetLogsHandler)}
        };
        public IRequestHandler<IRequestOutput, IRequestInput> GetHandler(string endpoint)
        {
            return (IRequestHandler<IRequestOutput, IRequestInput>)service.GetService(Handlers[endpoint]);
        }
    }
}
