namespace XhO_OKit.DataBinding
{
    public class BindableProperty<T>
    {
        public delegate void ValueChangedHandler(T oldValue, T newValue);

        public ValueChangedHandler OnValueChanged;
        //!!!这个是有默认值的
        //初始值尽量设置诸如0等 可以0.001f;
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                //if (!Equals(_value, value))
                //{
                    T old = _value;
                    _value = value;
                    ValueChanged(old, _value);
                //}
            }
        }

        private void ValueChanged(T oldValue, T newValue)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(oldValue, newValue);
            }
        }

        public override string ToString()
        {
            return (Value != null ? Value.ToString() : "null");
        }
    }
}
