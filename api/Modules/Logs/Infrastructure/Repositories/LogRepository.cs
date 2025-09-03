using Api.Modules.Logs.Domain;
using Api.Shared.DB;
using System.Reflection;

namespace Api.Modules.Logs.Infrastructure.Repositories
{
    public class LogRepository(ApiDbContext context)
    {
        private readonly ApiDbContext _context  = context;
        public async Task Create(Log log)
        {
            await _context.AddAsync(log);
            _context.SaveChanges();
        }
        public List<Log> GetAll()
        {
            PropertyInfo sortProperty = typeof(Log).GetProperty("TimeStamp")!;

            IOrderedEnumerable<Log> sortedLogs = _context.Logs.OrderByDescending(sortProperty.GetValue);

            return [.. sortedLogs];
        }
    }
}
