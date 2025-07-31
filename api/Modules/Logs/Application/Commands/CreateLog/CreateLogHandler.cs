using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Presentation.LogDTOs;
using Api.Shared.Interfaces;

namespace Api.Modules.Logs.Application.Commands.CreateLog
{
    public class CreateLogHandler(LogRepository repository) : IRequestHandler<IRequestOutput, IRequestInput>
    {
        private static readonly Lock _lock = new();
        public IRequestOutput Handle(IRequestInput input)
        {
            var command = (CreateLogCommand)input;
            LogValidator validator = new(command.Log);

            if (!validator.ValidateLog())
                return new CreateLogResponse(message: "invalid data");

            command.Log.Id = Guid.NewGuid();
            command.Log.UserId = command.UserId;
            command.Log.TimeStamp = DateTime.Now;

            lock (_lock)
            {
                repository.Create(LogDtoMapper.ToEntity(command.Log));
            }

            return new CreateLogResponse();
        }
    }
}
