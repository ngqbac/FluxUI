using Cysharp.Threading.Tasks;

namespace FluxUI.UIElements
{
    public interface IUIElement
    {
        string ElementId { get; }
        UniTask RevealAsync();
        UniTask ConcealAsync();
        bool IsVisible { get; }
    }
}