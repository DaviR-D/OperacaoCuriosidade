using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientResponse(string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
    }
}
