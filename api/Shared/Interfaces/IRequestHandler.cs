namespace Api.Shared.Interfaces
{
    public interface IRequestHandler<Output, Input>
    {
        Output HandleAsync(Input input);
    }
}