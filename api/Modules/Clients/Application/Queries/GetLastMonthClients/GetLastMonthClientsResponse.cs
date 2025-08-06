using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetLastMonthClients
{
    public class GetLastMonthClientsResponse(int lastMonth, string? message = null) : IRequestOutput
    {
        public int Length { get; set; } = lastMonth;
        public string? Message { get; set; } = message;
    }
}
