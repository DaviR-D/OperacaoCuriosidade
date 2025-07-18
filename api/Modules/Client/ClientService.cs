using System.Reflection;

namespace Api.Modules.Clients
{
    public class ClientService(List<Client> clients) : IClientService
    {
        private static readonly Lock _lock = new();

        public void CreateClient(ClientDto client)
        {
            lock (_lock)
            {
                client.Id = Guid.NewGuid();
                client.Date = DateTime.Now;
                if(VerifyAvailableEmail(client.Id, client.Email)) clients.Add(DtoMapper.ToEntity(client));
            }
        }
        public ClientDto GetSingleClient(Guid id)
        {
            ClientDto client = DtoMapper.ToDto(clients.First(c => c.Id == id));
            return client;
        }

        public List<ClientPreviewDto> GetPagedClients(int start, int increment, List<Client> clients)
        {
            List<Client> activeClients = [.. clients.Where(client => client.Deleted == false)];
            List<Client> slicedClients = [.. activeClients.Skip(start).Take(increment)];
            List<ClientPreviewDto> page = DtoMapper.ToPreviewDto(slicedClients);

            return page;
        }

        public void UpdateClient(ClientDto client)
        {
            int clientIndex = clients.FindIndex(r => r.Id == client.Id);
            clients[clientIndex] = DtoMapper.ToEntity(client);
        }

        public void DeleteClient(Guid id)
        {
            Client client = clients.First(c => c.Id == id);
            client.Deleted = true;
        }

        public bool VerifyAvailableEmail(Guid id, string email)
        {
            Client? existingEmail = clients.FirstOrDefault(client => client.Email == email);
            if (existingEmail != null)
            {
                return existingEmail.Id.Equals(id);
            }
            return true;
        }

        public List<ClientPreviewDto> GetSortedClients(string sortKey, bool descending, int start, int increment)
        {
            List<Client> activeClients = [.. clients.Where(client => client.Deleted == false)];
            PropertyInfo? sortProperty = sortKey == "default" ? typeof(Client).GetProperty("Id") : typeof(Client).GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            IOrderedEnumerable<Client> sortedClients = descending ? activeClients.OrderByDescending(sortProperty.GetValue) : activeClients.OrderBy(sortProperty.GetValue);

            return GetPagedClients(start, increment, [.. sortedClients]);
        }

        public List<ClientPreviewDto> SearchClients(string query, int start, int increment)
        {
            List<Client> activeClients = [.. clients.Where(client => client.Deleted == false)];
            List<Client> filteredClients = [.. activeClients
                .Where(client => $"{client.Name} {client.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];

            return GetPagedClients(start, increment, filteredClients);
        }
        public ClientsStatsDto GetClientsStats()
        {
            List<Client> activeClients = [.. clients.Where(client => client.Deleted == false)];
            int lastMonth = activeClients.Where(client => client.Date >= DateTime.Now.AddMonths(-1)).Count();
            int pending = activeClients.Where(client => client.Pending == true).Count();
            return new ClientsStatsDto(activeClients.Count, lastMonth, pending);
        }
    }
}