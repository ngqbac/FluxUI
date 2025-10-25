using System;
using System.Collections.Generic;

namespace FluxUI.Binding
{
    public class ReactiveProperty<T>
    {
        private T _value;
        private readonly List<Action<T>> _observers = new();

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;
                _value = value;
                foreach (var o in _observers) o(_value);
            }
        }

        public IDisposable Subscribe(Action<T> observer)
        {
            _observers.Add(observer);
            observer(_value);
            return Disposable.Create(() => _observers.Remove(observer));
        }
    }
}