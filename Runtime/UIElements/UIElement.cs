using FluxUI.Binding;
using FluxUI.Logging;
using OrganicBeing.Integration.Unity;

namespace FluxUI.UIElements
{
    public abstract class UIElement : MonoOrganic
    {
        public readonly EventCommand OnWillRevealCommand = new();
        public readonly EventCommand OnDidRevealCommand = new();
        public readonly EventCommand OnWillConcealCommand = new();
        public readonly EventCommand OnDidConcealCommand = new();
        
        public bool IsVisible { get; private set; }

        public virtual void Reveal() => WhenReady(() =>
        {
            OnWillReveal();
            gameObject.SetActive(true);
            IsVisible = true;
            OnDidReveal();
        });

        public virtual void Conceal() => WhenReady(() =>
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

        protected void Verbose(object message) => FluxUILogger.Verbose(message);
    }
}