using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetPendingClients
{
    public class GetPendingClientsHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            List<Client> activeClients = await repository.GetAllAsync();
            int pending = activeClients.Where(client => client.Pending == true).Count();
            return new GetPendingClientsResponse(pending);
        }
    }
}
