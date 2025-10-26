using Cysharp.Threading.Tasks;
using FluxUI.Binding;
using FluxUI.Logging;
using OrganicBeing.Integration.Async;
using OrganicBeing.Integration.Unity;

namespace FluxUI.UIElements
{
    public abstract class UIElement : MonoOrganic, IUIElement
    {
        public readonly EventCommand OnWillRevealCommand = new();
        public readonly EventCommand OnDidRevealCommand = new();
        public readonly EventCommand OnWillConcealCommand = new();
        public readonly EventCommand OnDidConcealCommand = new();
        
        public bool IsVisible { get; private set; }

        public string ElementId { get; protected set; }
        
        public void Reveal() => RevealAsync().Forget();

        public void Conceal() => ConcealAsync().Forget();

        public virtual async UniTask RevealAsync() => await this.WhenReadyAsync(() =>
        {
            OnWillReveal();
            gameObject.SetActive(true);
            IsVisible = true;
            OnDidReveal();
        });

        public virtual async UniTask ConcealAsync() => await this.WhenReadyAsync(() =>
        {
            OnWillConceal();
            gameObject.SetActive(false);
            IsVisible = false;
            OnDidConceal();
        });

        protected virtual void OnWillReveal()
        {
            OnWillRevealCommand.Invoke();
        }

        protected virtual void OnDidReveal()
        {
            OnDidRevealCommand.Invoke();
        }

        protected virtual void OnWillConceal()
        {
            OnWillConcealCommand.Invoke();
        }

        protected virtual void OnDidConceal()
        {
            OnDidConcealCommand.Invoke();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            Conceal();
            OnWillRevealCommand.Clear();
            OnDidRevealCommand.Clear();
            OnWillConcealCommand.Clear();
            OnDidConcealCommand.Clear();
        }

        protected static void Verbose(object message) => FluxUILogger.Verbose(message);
    }
}