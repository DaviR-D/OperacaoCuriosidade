using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Modules.Logs.Infrastructure;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientHandler(ClientRepository repository, CreateLogService logService) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var query = (GetSingleClientQuery)input;
            var client = repository.GetOne(query.Id);
            if (client == null || client.Deleted == true)
            {
                return new GetSingleClientResponse(message: "client does not exist");
            }

            logService.Create(userId: query.UserId, clientId: query.Id, action: "Read");
            return new GetSingleClientResponse(client: ClientDtoMapper.ToDto(client)); ;
        }
    }
}
