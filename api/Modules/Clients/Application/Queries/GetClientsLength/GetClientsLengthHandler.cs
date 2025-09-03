using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetClientsLength
{
    public class GetClientsLengthHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            List<Client> activeClients = await repository.GetAllAsync();
            return new GetClientsLengthResponse(activeClients.Count);
        }
    }
}
