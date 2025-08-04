using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetSingleClient
{
    public class GetSingleClientQuery(Guid id, Guid userId) : IRequestInput
    {
        public Guid Id { get; set; } = id;
        public Guid UserId { get; set; } = userId;
    }
}
