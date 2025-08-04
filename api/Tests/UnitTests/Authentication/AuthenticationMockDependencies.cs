using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Authentication.Presentation;
using Api.Shared.Configurations;

namespace Api.Tests.UnitTests.Authentication
{
    public class AuthenticationMockDependencies
    {
        public IServiceProvider ServiceProvider { get; set; }
        public List<User> UsersMock { get; set; } = [];

        public AuthenticationMockDependencies()
        {
            var services = new ServiceCollection();
            AuthenticationSettings? mockAuthSettings = new();
            mockAuthSettings.PrivateKey = "mockprivatekeyforunittesting12345678910";
            services.AddSingleton(UsersMock);
            services.AddSingleton(mockAuthSettings);
            services.AddScoped<AuthenticationHandlerFactory>();
            services.AddScoped<UserRepository>();
            services.AddScoped<AuthenticateHandler>();
            services.AddScoped<CreateUserHandler>();
            services.AddScoped<AuthenticationController>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
