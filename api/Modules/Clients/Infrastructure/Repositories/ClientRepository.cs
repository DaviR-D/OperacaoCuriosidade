using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Presentation.ClientDTOs;
using Api.Shared.DB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.Modules.Clients.Infrastructure.Repositories
{
    public class ClientRepository(ApiDbContext context)
    {
        private readonly ApiDbContext _context = context;
        public async Task<Guid?> CreateAsync(ClientDto client)
        {
            client.Id = Guid.NewGuid();
            client.Date = DateTime.Now;
            _context.Clients.Add(ClientDtoMapper.ToEntity(client));
            await _context.SaveChangesAsync();

            return client.Id;
        }
        public async Task UpdateAsync(ClientDto client)
        {
            Client storedClient = await _context.Clients.FindAsync(client.Id);
            client.Date = storedClient.Date;
            Client updatedClient = ClientDtoMapper.ToEntity(client);
            _context.Entry(storedClient).CurrentValues.SetValues(updatedClient);

            await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(Guid id)
        {
            Client? client = await _context.Clients.FindAsync(id);

            if (client == null || client.Deleted)
                return true;

            client.Deleted = true;
            await _context.SaveChangesAsync();

            return false;
        }
        public async Task<Client?> GetOneAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }
        public async Task<List<Client>> GetAllAsync()
        {
            var activeClients = await GetActiveClients();

            return [.. activeClients];
        }
        public async Task<List<Client>> SearchAsync(string query)
        {
            var activeClients = await GetActiveClients();

            return [.. activeClients
                .Where(client => $"{client.Name} {client.Email.Split("@")[0]}"
                .Contains(query, StringComparison.CurrentCultureIgnoreCase))];
        }
        public async Task<List<Client>> Sort(string sortKey, bool descending)
        {
            var activeClients = await GetActiveClients();

            PropertyInfo? sortProperty = typeof(Client)
                .GetProperty(sortKey, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            IOrderedEnumerable<Client> sortedClients =
                descending ?
                activeClients.OrderByDescending(sortProperty.GetValue) :
                activeClients.OrderBy(sortProperty.GetValue);

            return [.. sortedClients];
        }
        public async Task LockAsync(Guid id, DateTime editLock)
        {
            Client? client = await _context.Clients.FindAsync(id);
            client.EditLock = editLock;

            await _context.SaveChangesAsync();
        }
        public async Task UnlockAsync(Guid id)
        {
            Client? client = await _context.Clients.FindAsync(id);
            client.EditLock = null;

            await _context.SaveChangesAsync();
        }
        private async Task<List<Client>> GetActiveClients()
        {
            return await context.Clients.Where(client => client.Deleted == false).ToListAsync();
        }
    }
}
