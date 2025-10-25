namespace FluxUI.Arguments
{
    public interface IUIArgument
    {
        string Id { get; }
        bool IsValid(out string reason);
    }
}