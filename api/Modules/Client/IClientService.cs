namespace Api.Modules.Clients
{
    public interface IClientService
    {
        void CreateClient(ClientDto client);
        ClientDto GetSingleClient(Guid id);
        List<ClientPreviewDto> GetPagedClients(int start, int increment, List<Client> clients);
        List<ClientPreviewDto> GetSortedClients(string sortKey, bool descending, int start, int increment);
        List<ClientPreviewDto> SearchClients(string query, int start, int increment);
        void UpdateClient(ClientDto client);
        void DeleteClient(Guid id);
        bool VerifyAvailableEmail(Guid id, string email);
        ClientsStatsDto GetClientsStats();
    }
}