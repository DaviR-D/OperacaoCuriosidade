using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.CreateClient
{
    public class CreateClientResponse(Guid? id = null, string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
        public Guid? Id { get; set; } = id;
    }
}
