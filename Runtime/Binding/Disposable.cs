using System;
using FluxUI.ViewModels;

namespace FluxUI.Binding
{
    public static class Disposable
    {
        public static IDisposable Create(Action onDispose)
        {
            return new ActionDisposable(onDispose);
        }

        private class ActionDisposable : IDisposable
        {
            private readonly Action _onDispose;
            private bool _disposed;

            public ActionDisposable(Action onDispose)
            {
                _onDispose = onDispose;
            }

            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                _onDispose?.Invoke();
            }
        }
    }
}