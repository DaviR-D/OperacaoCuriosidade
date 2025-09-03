using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.SearchClients
{
    public class SearchClientsHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var query = (SearchClientsQuery)input;
            List<Client> filteredClients = await repository.SearchAsync(query.Query);
            List<Client> slicedClients = [.. filteredClients.Skip(query.Start).Take(query.Increment)];
            var response = new SearchClientsResponse(
                page: ClientDtoMapper.ToPreviewDto(slicedClients),
                resultsLength: filteredClients.Count
                );

            return response;
        }
    }
}
