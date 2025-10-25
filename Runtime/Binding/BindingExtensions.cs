using FluxUI.ViewModels;
using UnityEngine.UI;

namespace FluxUI.Binding
{
    public static class BindingExtensions
    {
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