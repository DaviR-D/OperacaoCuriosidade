using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetClientsLength
{
    public class GetClientsLengthHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            List<Client> activeClients = repository.GetAll();
            return new GetClientsLengthResponse(activeClients.Count);
        }
    }
}
