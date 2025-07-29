using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Commands.LockClient
{
    public class LockClientResponse(string? token = null, string? message = null) : IRequestOutput
    {
        public string? Message { get; set; } = message;
        public string? Token { get; set; } = token;
    }
}
