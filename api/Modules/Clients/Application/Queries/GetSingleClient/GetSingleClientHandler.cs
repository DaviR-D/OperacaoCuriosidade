using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var query = (GetSingleClientQuery)input;
            var client = await repository.GetOneAsync(query.ClientId);

            if (client == null || client.Deleted == true)
            {
                return new GetSingleClientResponse(message: "client does not exist");
            }

            logService.Create(userId: query.UserId, clientId: query.ClientId, action: "Read");
            return new GetSingleClientResponse(client: ClientDtoMapper.ToDto(client));
        }
    }
}
