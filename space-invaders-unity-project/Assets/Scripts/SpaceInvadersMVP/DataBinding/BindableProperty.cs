using System;

namespace SpaceInvadersMVP.DataBinding
{
    public class BindableProperty<T>
    {
        public event Action<T> ValueChanged;

        public T Value => _value;
        protected T _value;

        public BindableProperty(T value = default)
        {
            _value = value;
        }

        protected void InvokeValueChanged()
        {
            ValueChanged?.Invoke(_value);
        }

        public void Subscribe(Action<T> listener)
        {
            ValueChanged += listener;
            listener.Invoke(_value);
        }

        public void Unsubscribe(Action<T> listener)
        {
            ValueChanged -= listener;
        }
    }
}
