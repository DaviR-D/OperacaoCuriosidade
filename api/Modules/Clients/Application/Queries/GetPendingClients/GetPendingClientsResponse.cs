using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetPendingClients
{
    public class GetPendingClientsResponse(int pending, string? message = null) : IRequestOutput
    {
        public int Length { get; set; } = pending;
        public string? Message { get; set; } = message;
    }
}
