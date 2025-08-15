using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;
using System.Threading.Tasks;

namespace Api.Modules.Clients.Application.Queries.GetLastMonthClients
{
    public class GetLastMonthClientsHandler(ClientRepository repository) : IRequestHandler<Task<IRequestOutput>, IRequestInput>
    {
        public async Task<IRequestOutput> HandleAsync(IRequestInput input)
        {
            List<Client> activeClients = await repository.GetAllAsync();
            int lastMonth = activeClients
                .Where(client => client.Date >= DateTime.Now
                .AddMonths(-1))
                .Count();

            return new GetLastMonthClientsResponse(lastMonth);
        }
    }
}
