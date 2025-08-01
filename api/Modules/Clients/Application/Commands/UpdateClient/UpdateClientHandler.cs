using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UpdateClient
{
    public class UpdateClientHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (UpdateClientCommand)input;
            var client = repository.GetOne(command.ClientId);

            if (client.Lock?.ToString("yyyy-MM-dd HH:mm:ss.fff") != command.TokenExpireDate.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                return new UpdateClientResponse(message: "invalid token");

            command.Client.Id = command.ClientId;
            ClientValidator validator = new(command.Client);

            if (!validator.ValidateClient())
                return new UpdateClientResponse(message: "invalid data");

            repository.Update(command.Client);

            return new UpdateClientResponse();
        }
    }
}
