using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.VerifyAvailableEmail
{
    public class VerifyAvailableEmailHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            var query = (VerifyAvailableEmailQuery)input;
            var existingEmailTask = await repository.GetAllAsync();

            Client? existingEmail = existingEmailTask.FirstOrDefault(client => client.Email == query.Email);

            if (existingEmail != null)
            {
                return new VerifyAvailableEmailResponse(existingEmail.Id.Equals(query.ClientId));
            }

            return new VerifyAvailableEmailResponse(true);
        }
    }
}
