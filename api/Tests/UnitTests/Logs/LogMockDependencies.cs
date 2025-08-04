using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Domain;
using Api.Modules.Logs.Application;
using Api.Modules.Logs.Application.Commands.CreateLog;
using Api.Modules.Logs.Application.Queries.GetLogs;
using Api.Modules.Logs.Domain;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation;
using Api.Shared.Configurations;

namespace Api.Tests.UnitTests.Logs
{
    public class LogMockDependencies
    {
        public IServiceProvider ServiceProvider { get; set; }
        public List<User> UsersMock { get; set; } = [];
        public List<Client> ClientsMock { get; set; } = [];
        public List<Log> LogsMock { get; set; } = [];

        public LogMockDependencies()
        {
            var services = new ServiceCollection();
            AuthenticationSettings? mockAuthSettings = new();
            mockAuthSettings.PrivateKey = "mockprivatekeyforunittesting12345678910";
            services.AddSingleton(UsersMock);
            services.AddSingleton(ClientsMock);
            services.AddSingleton(LogsMock);
            services.AddSingleton(mockAuthSettings);
            services.AddScoped<LogHandlerFactory>();
            services.AddScoped<LogRepository>();
            services.AddScoped<CreateLogHandler>();
            services.AddScoped<GetLogsHandler>();
            services.AddScoped<LogController>();


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
