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
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application
{
    public class ClientsHandlerFactory(IServiceProvider service)
    {
        private static readonly Dictionary<string, Type> Handlers = new()
        {
            { "Create", typeof(CreateClientHandler) },
            { "Lock", typeof(LockClientHandler) },
            { "Unlock", typeof(UnlockClientHandler) },
            { "GetSingle", typeof(GetSingleClientHandler) },
            { "GetLength", typeof(GetClientsLengthHandler) },
            { "GetLastMonth", typeof(GetLastMonthClientsHandler) },
            { "GetPending", typeof(GetPendingClientsHandler) },
            { "GetPage", typeof(GetPagedClientsHandler) },
            { "GetSortedPage", typeof(GetSortedClientsHandler) },
            { "SearchClients", typeof(SearchClientsHandler) },
            { "Update", typeof(UpdateClientHandler) },
            { "Delete", typeof(DeleteClientHandler) },
            { "CheckEmail", typeof(VerifyAvailableEmailHandler) }
        };

        public IRequestHandler<Task<IRequestOutput>, IRequestInput> GetHandler(string endpoint)
        {
            return (IRequestHandler<Task<IRequestOutput>, IRequestInput>)service.GetService(Handlers[endpoint]);
        }
    }
}
