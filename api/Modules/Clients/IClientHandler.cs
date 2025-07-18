namespace Api.Modules.Clients
{
    public interface IClientHandler<Output, Input>
    {
        Output Handle(Input input);
    }
}