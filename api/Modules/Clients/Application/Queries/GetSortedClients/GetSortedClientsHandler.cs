using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;
using System.Reflection;

namespace Api.Modules.Clients.Application.Queries.GetSortedClients
{
    public class GetSortedClientsHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        public IClientOutput Handle(IClientInput input)
        {
            var query = (GetSortedClientsQuery)input;
            List<Client> sortedClients = repository.Sort(query.SortKey, query.Descending);
            List<Client> slicedClients = [.. sortedClients.Skip(query.Start).Take(query.Increment)];
            var response = new GetSortedClientsResponse(DtoMapper.ToPreviewDto(slicedClients));

            return response;
        }
    }
}
