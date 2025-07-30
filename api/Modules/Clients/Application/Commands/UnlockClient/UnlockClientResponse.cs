using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.UnlockClient
{
    public class UnlockClientResponse(string? newToken = null, string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
        public string? NewToken { get; set; } = newToken;
    }
}
