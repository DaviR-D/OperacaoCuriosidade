using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared;

namespace Api.Modules.Logs.Application.Commands
{
    public class LogValidator(CreateLogDto log) : ValidatorBase
    {
        private readonly CreateLogDto _log = log;

        public bool ValidateLog()
        {
            return ValidateFieldSize(_log.Action);
        }
    }
}
