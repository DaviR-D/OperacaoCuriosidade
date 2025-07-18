using Api.Modules.Clients;
using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.Handlers
{
    public class GetSingleClientHandler(ClientRepository repository)
    {
        public ClientDto Handle(Guid id)
        {
            return repository.GetOne(id);
        }
    }
}
