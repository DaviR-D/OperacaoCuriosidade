using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSortedClients
{
    public class GetSortedClientsQuery(string sortKey, bool descending, int start, int increment) : IClientInput
    {
        public string SortKey { get; set; } = sortKey;
        public bool Descending { get; set; } = descending;
        public int Start { get; set; } = start;
        public int Increment { get; set; } = increment;
    }
}
