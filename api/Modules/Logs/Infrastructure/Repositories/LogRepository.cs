using Api.Modules.Logs.Domain;
using System.Reflection;

namespace Api.Modules.Logs.Infrastructure.Repositories
{
    public class LogRepository(List<Log> logs)
    {
        public void Create(Log log)
        {
            logs.Add(log);
        }
        public List<Log> GetAll()
        {
            PropertyInfo? sortProperty = typeof(Log).GetProperty("TimeStamp");
            IOrderedEnumerable<Log> sortedLogs = logs.OrderByDescending(sortProperty.GetValue);

            return [.. sortedLogs];
        }
    }
}
