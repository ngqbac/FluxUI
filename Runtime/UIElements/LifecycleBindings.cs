using FluxUI.ViewModels;

namespace FluxUI.UIElements
{
    public static class LifecycleBindings
    {
        public static void BindLifecycleTo(this UIElement element, UIViewModel viewModel)
        {
            element.OnWillRevealCommand.Subscribe(() => viewModel.OnUIWillReveal()).AddTo(viewModel);
            element.OnDidRevealCommand.Subscribe(() => viewModel.OnUIDidReveal()).AddTo(viewModel);
            element.OnWillConcealCommand.Subscribe(() => viewModel.OnUIWillConceal()).AddTo(viewModel);
            element.OnDidConcealCommand.Subscribe(() => viewModel.OnUIDidConceal()).AddTo(viewModel);
        }
    }
}