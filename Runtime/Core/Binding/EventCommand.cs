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
        
        public void Clear()
        {
            _listeners.Clear();
        }
    }
    
    public class EventCommand<T>
    {
        private readonly List<Action<T>> _listeners = new();

        public void Invoke(T value)
        {
            foreach (var l in _listeners)
                l(value);
        }

        public IDisposable Subscribe(Action<T> listener)
        {
            _listeners.Add(listener);
            return Disposable.Create(() => _listeners.Remove(listener));
        }
        
        public void Clear()
        {
            _listeners.Clear();
        }
    }
}