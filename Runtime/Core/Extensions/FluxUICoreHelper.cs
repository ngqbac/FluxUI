using FluxUI.Binding;
using FluxUI.Core;
using FluxUI.ViewModels;
using UnityEngine.UI;

namespace FluxUI.Extensions
{
    public static class FluxUICoreHelper
    {
        public static void BindLifecycleTo(this UIElement element, UIViewModel viewModel)
        {
            element.OnWillRevealCommand.Subscribe(viewModel.OnUIWillReveal).AddTo(viewModel);
            element.OnDidRevealCommand.Subscribe(viewModel.OnUIDidReveal).AddTo(viewModel);
            element.OnWillConcealCommand.Subscribe(viewModel.OnUIWillConceal).AddTo(viewModel);
            element.OnDidConcealCommand.Subscribe(viewModel.OnUIDidConceal).AddTo(viewModel);
        }
        
        public static void BindText<TViewModel>(
            this TViewModel viewModel,
            ReactiveProperty<string> prop,
            Text uiText
        ) where TViewModel : IViewModel
        {
            prop.Subscribe(value => uiText.text = value).AddTo(viewModel);
        }
        
        public static void BindInteractable<TViewModel>(
            this TViewModel viewModel,
            ReactiveProperty<bool> prop,
            Selectable ui
        ) where TViewModel : IViewModel
        {
            prop.Subscribe(value => ui.interactable = value).AddTo(viewModel);
        }

        public static void BindButton<TViewModel>(
            this TViewModel viewModel,
            EventCommand command,
            Button button
        ) where TViewModel : IViewModel
        {
            button.onClick.AddListener(command.Invoke);
        }
        
        public static void BindButtonWithPayload<TViewModel, TPayload>(
            this TViewModel viewModel,
            EventCommand<TPayload> command,
            Button button,
            TPayload payload
        ) where TViewModel : IViewModel
        {
            button.onClick.AddListener(() => command.Invoke(payload));
        }
    }
}