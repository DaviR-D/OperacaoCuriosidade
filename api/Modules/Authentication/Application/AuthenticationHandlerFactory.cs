using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Shared.Interfaces;

namespace Api.Modules.Authentication.Application
{
    public class AuthenticationHandlerFactory(IServiceProvider service)
    {
        public IRequestHandler<IRequestOutput, IRequestInput> GetHandler(string endpoint)
        {
            IRequestHandler<IRequestOutput, IRequestInput> handler;

            if (endpoint == "/api/authentication/signup")
            {
                handler = service.GetService<CreateUserHandler>();
                return handler;
            } else if (endpoint == "/api/authentication/")
            {
                handler = service.GetService<AuthenticateHandler>();
                return handler;
            }
            return null;
        }
    }
}
