using Api.Modules.Clients.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailResponse(bool isAvailable) : IClientOutput
    {
        public bool IsAvailable { get; set; } = isAvailable;
    }
}
