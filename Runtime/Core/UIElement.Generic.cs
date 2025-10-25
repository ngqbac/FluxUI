using FluxUI.Arguments;
using FluxUI.ViewModels;
using OrganicBeing.Core;

namespace FluxUI.Core
{
    public abstract class UIElement<TArg, TViewModel> : UIElement, IOrganicHost<TArg>
        where TArg : IUIArgument
        where TViewModel : IViewModel, new()
    {
        public TArg Data { get; set; }
        public void Absorb(TArg data) => Data = data;
        
        protected TViewModel ViewModel { get; private set; }
        
        protected override void OnGrow()
        {
            ViewModel = new TViewModel();
            OnInitializeViewModel();
        }
        
        protected virtual void OnInitializeViewModel() { }
        
        protected override void OnWillReveal()
        {
            base.OnWillReveal();
            ViewModel?.Activate();
        }
        
        protected override void OnWillConceal()
        {
            base.OnWillConceal();
            ViewModel?.Deactivate();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            ViewModel = default;
            Data = default;
        }
    }
}