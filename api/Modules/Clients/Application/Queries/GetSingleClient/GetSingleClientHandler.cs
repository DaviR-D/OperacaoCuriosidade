using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Presentation.ClientDTOs;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository) : IClientHandler<IClientOutput, IClientInput>
    {
        public IClientOutput Handle(IClientInput input)
        {
            var query = (GetSingleClientQuery)input;
            var response = new GetSingleClientResponse(repository.GetOne(query.Id)); 
            return response;
        }
    }
}
