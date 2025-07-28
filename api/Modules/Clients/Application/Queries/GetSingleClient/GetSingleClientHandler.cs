using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (GetSingleClientQuery)input;
            var client = repository.GetOne(query.Id);
            if (client == null || client.Deleted == true)
            {
                return new GetSingleClientResponse(message: "client does not exist");
            }

            return new GetSingleClientResponse(client: client); ;
        }
    }
}
