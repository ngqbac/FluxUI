using System;

namespace FluxUI.ViewModels
{
    public static class ViewModelExtensions
    {
        public static IDisposable AddTo(this IDisposable disposable, IViewModel viewModel)
        {
            viewModel.AddBinding(disposable);
            return disposable;
        }
    }
}