using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogCommand(LogDto log, Guid userId) : IRequestInput
    {
        public LogDto Log = log;
        public Guid UserId { get; set; } = userId; 
    }
}
