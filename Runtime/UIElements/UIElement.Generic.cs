using FluxUI.Arguments;
using FluxUI.Logging;
using FluxUI.ViewModels;
using OrganicBeing.Core;

namespace FluxUI.UIElements

{
    public abstract class UIElement<TArg, TViewModel> : UIElement, IOrganicHost<TArg>
        where TArg : IUIArgument
        where TViewModel : UIViewModel, new()
    {
        public TArg Data { get; set; }
        public void Absorb(TArg data)
        {
            if (data.IsValid(out var reason))
            {
                Data = data;
                OnDidAbsorb(data, wasFallbackAttempted: false);
                return;
            }

            FluxUILogger.Warn($"Absorb failed: {reason}. Trying to fallback.");

            var fallback = GetFallbackArgument();
            var reasonFallback = "";
            if (fallback != null && fallback.IsValid(out reasonFallback))
            {
                FluxUILogger.Warn($"Fallback argument used: {fallback.Id} instead of invalid original.");
                Data = fallback;
                OnDidAbsorb(fallback, wasFallbackAttempted: true);
                return;
            }

            FluxUILogger.Error($"Absorb failed: both original and fallback arguments were invalid.\n{reasonFallback}");
            OnAbsorbFailed(data, wasFallbackAttempted: true);
        }

        protected virtual void OnAbsorbFailed(TArg data, bool wasFallbackAttempted) { }
        protected virtual void OnDidAbsorb(TArg data, bool wasFallbackAttempted) { }
        protected virtual TArg GetFallbackArgument() => default;
        
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
            ViewModel?.Bind();
        }
        
        protected override void OnWillConceal()
        {
            base.OnWillConceal();
            ViewModel?.Unbind();
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            ViewModel = null;
            Data = default;
        }
    }
}