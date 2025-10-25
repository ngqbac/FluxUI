using System;
using System.Collections.Generic;

namespace FluxUI.Binding
{
    public class EventCommand
    {
        private readonly List<Action> _listeners = new();

        public void Invoke()
        {
            foreach (var l in _listeners) l();
        }

        public IDisposable Subscribe(Action listener)
        {
            _listeners.Add(listener);
            return Disposable.Create(() => _listeners.Remove(listener));
        }
    }
}