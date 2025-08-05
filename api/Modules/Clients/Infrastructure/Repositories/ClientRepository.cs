using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation.ClientDTOs;
using System.Reflection;

namespace Api.Modules.Clients.Infrastructure.Repositories
{
    public class ClientRepository(List<Client> clients)
    {
        private readonly List<Client> _activeClients = [.. clients.Where(client => client.Deleted == false)];
        public Guid? Create(ClientDto client)
        {
            client.Id = Guid.NewGuid();
            client.Date = DateTime.Now;
            clients.Add(ClientDtoMapper.ToEntity(client));
            return client.Id;
        }
        public void Update(ClientDto client)
        {
            int clientIndex = clients.FindIndex(r => r.Id == client.Id);
            client.Date = clients[clientIndex].Date;
            clients[clientIndex] = ClientDtoMapper.ToEntity(client);
        }
        public bool Delete(Guid id)
        {
            Client? client = clients.FirstOrDefault(c => c.Id == id);
            if (client == null || client.Deleted)
                return true;
            client.Deleted = true;
            return false;
        }
        public Client? GetOne(Guid id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }
        public List<Client> GetAll()
        {
            return [.. _activeClients];
        }
        public List<Client> Search(string query)
        {
            return [.. _activeClients
                .Where(client => $"{client.Name} {client.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];
        }
        public List<Client> Sort(string sortKey, bool descending)
        {
            PropertyInfo? sortProperty = typeof(Client)
                .GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            IOrderedEnumerable<Client> sortedClients =
                descending ?
                _activeClients.OrderByDescending(sortProperty.GetValue) :
                _activeClients.OrderBy(sortProperty.GetValue);

            return [.. sortedClients];
        }
    }
}
