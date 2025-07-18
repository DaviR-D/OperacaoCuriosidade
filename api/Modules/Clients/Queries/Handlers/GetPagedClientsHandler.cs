using Api.Modules.Clients;
using Api.Modules.Repositories;

namespace Api.Modules.Clients.Queries.Handlers
{
    public class GetPagedClientsHandler(ClientRepository repository)
    {
        public List<ClientPreviewDto> Handle(int start, int increment)
        {
            List<Client> activeClients = repository.GetAll();
            List<Client> slicedClients = [.. activeClients.Skip(start).Take(increment)];
            List<ClientPreviewDto> page = DtoMapper.ToPreviewDto(slicedClients);

            return page;
        }
    }
}
