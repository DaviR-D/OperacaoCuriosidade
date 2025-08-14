using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetPendingClients
{
    public class GetPendingClientsHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput HandleAsync(IRequestInput input)
        {
            List<Client> activeClients = repository.GetAll();
            int pending = activeClients.Where(client => client.Pending == true).Count();
            return new GetPendingClientsResponse(pending);
        }
    }
}
