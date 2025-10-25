using System;

namespace FluxUI.ViewModels
{
    public interface IViewModel
    {
        void AddBinding(IDisposable disposable);
    }
}