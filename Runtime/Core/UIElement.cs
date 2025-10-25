using OrganicBeing.Integration.Unity;

namespace FluxUI.Core
{
    public abstract class UIElement : MonoOrganic
    {
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

        protected virtual void OnWillReveal() { }
        protected virtual void OnDidReveal() { }
        protected virtual void OnWillConceal() { }
        protected virtual void OnDidConceal() { }

        public override void OnRecycle()
        {
            base.OnRecycle();
            Conceal();
        }
    }
}