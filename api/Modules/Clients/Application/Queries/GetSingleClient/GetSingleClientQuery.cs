using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientQuery(Guid id) : IClientInput
    {
        public Guid Id { get; set; } = id;
    }
}
