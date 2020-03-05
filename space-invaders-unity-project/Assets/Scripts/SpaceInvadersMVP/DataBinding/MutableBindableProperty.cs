using System.Collections.Generic;

namespace SpaceInvadersMVP.DataBinding
{
    public class MutableBindableProperty<T> : BindableProperty<T>
    {
        public MutableBindableProperty(T value = default) : base(value) { }

        public new T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    _value = value;
                    InvokeValueChanged();
                }
            }
        }
    }
}
