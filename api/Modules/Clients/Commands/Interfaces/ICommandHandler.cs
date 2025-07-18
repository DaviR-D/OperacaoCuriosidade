namespace Api.Modules.Clients.Commands.Interfaces
{
    public interface ICommandHandler : IClientHandler<ICommandResponse, IClientCommand>
    {
        ICommandResponse Handle(IClientCommand command);
    }
}
