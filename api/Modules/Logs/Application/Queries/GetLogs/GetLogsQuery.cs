using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Queries.GetLogs
{
    public class GetLogsQuery(int start, int increment) : IRequestInput
    {
        public int Start { get; set; } = start;
        public int Increment { get; set; } = increment;
    }
}
