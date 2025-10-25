using System;
using System.Collections.Generic;
using FluxUI.Binding;
using FluxUI.ViewModels;

namespace FluxUI.ViewModels
{
    public abstract class UIViewModel: IViewModel
    {
        private readonly List<IDisposable> _bindings = new();
        public void AddBinding(IDisposable disposable) => _bindings.Add(disposable);

        protected void Bind<T>(ReactiveProperty<T> prop, Action<T> onChanged)
        {
            _bindings.Add(prop.Subscribe(onChanged));
        }

        protected void Bind(EventCommand command, Action onInvoke)
        {
            _bindings.Add(command.Subscribe(onInvoke));
        }

        public void Bind() => OnBind();

        public void Unbind()
        {
            foreach (var b in _bindings)
                b.Dispose();
            _bindings.Clear();
        }

        protected virtual void OnBind() { }
        
        public virtual void OnUIWillReveal() { }
        public virtual void OnUIDidReveal() { }
        public virtual void OnUIWillConceal() { }
        public virtual void OnUIDidConceal() { }
    }
}