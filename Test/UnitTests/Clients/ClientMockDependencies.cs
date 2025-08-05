using Api.Modules.Clients.Application;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.LockClient;
using Api.Modules.Clients.Application.Commands.UnlockClient;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Queries.GetClientsLength;
using Api.Modules.Clients.Application.Queries.GetLastMonthClients;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Application.Queries.GetPendingClients;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation;
using Api.Modules.Logs.Domain;
using Api.Modules.Logs.Infrastructure;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Shared.Configurations;

namespace Test.UnitTests.Clients
{
    public class ClientMockDependencies
    {
        public IServiceProvider ServiceProvider { get; set; }
        public List<Client> ClientsMock { get; set; } = [];
        public List<Log> LogsMock { get; set; } = [];
        public ClientMockDependencies()
        {
            var services = new ServiceCollection();
            AuthenticationSettings? mockAuthSettings = new();
            mockAuthSettings.PrivateKey = "mockprivatekeyforunittesting12345678910";
            string key = mockAuthSettings.PrivateKey;
            services.AddSingleton(ClientsMock);
            services.AddSingleton(LogsMock);
            services.AddSingleton(mockAuthSettings);
            services.AddScoped<ClientsHandlerFactory>();
            services.AddScoped<ClientRepository>();
            services.AddScoped<GetPagedClientsHandler>();
            services.AddScoped<GetClientsLengthHandler>();
            services.AddScoped<GetLastMonthClientsHandler>();
            services.AddScoped<GetPendingClientsHandler>();
            services.AddScoped<GetSingleClientHandler>();
            services.AddScoped<GetSortedClientsHandler>();
            services.AddScoped<SearchClientsHandler>();
            services.AddScoped<VerifyAvailableEmailHandler>();
            services.AddScoped<CreateClientHandler>();
            services.AddScoped<DeleteClientHandler>();
            services.AddScoped<UpdateClientHandler>();
            services.AddScoped<LockClientHandler>();
            services.AddScoped<UnlockClientHandler>();
            services.AddScoped<ClientController>();
            services.AddScoped<CreateLogService>();
            services.AddScoped<LogRepository>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
