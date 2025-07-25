using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Shared.Interfaces;

namespace Api.Modules.Clients.Application.Queries.GetLastMonthClients
{
    public class GetLastMonthClientsHandler(ClientRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        public IRequestOutput Handle(IRequestInput input)
        {
            List<Client> activeClients = repository.GetAll();
            int lastMonth = activeClients.Where(client => client.Date >= DateTime.Now.AddMonths(-1)).Count();
            return new GetLastMonthClientsResponse(lastMonth);
        }
    }
}
