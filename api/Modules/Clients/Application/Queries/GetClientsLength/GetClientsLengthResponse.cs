using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetClientsLength
{
    public class GetClientsLengthResponse(int length, string? message = null) : IRequestOutput
    {
        public int Length { get; set; } = length;
        public string? Message { get; set; } = message;
    }
}
