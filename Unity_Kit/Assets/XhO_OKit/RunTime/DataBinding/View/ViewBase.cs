using UnityEngine;

namespace XhO_OKit.DataBinding
{
    public abstract class ViewBase<T>:MonoBehaviour,IView<T> where T:ViewModelBase
    {
        private bool _isInitialized;
        protected readonly PropertyBinder<T> Binder=new PropertyBinder<T>();
        public readonly BindableProperty<T> ViewModelProperty = new BindableProperty<T>();

        public T BindingContext
        {
            get { return ViewModelProperty.Value; }
            set
            {
                if (!_isInitialized)
                {
                    OnInitialize();
                    _isInitialized = true;
                }
                //触发OnValueChanged事件
                ViewModelProperty.Value = value;
            }
        }

        /// <summary>
        /// 当gameObject将被销毁时，这个方法被调用
        /// </summary>
        public virtual void OnDestroy()
        {
            BindingContext.OnDestory();
            BindingContext = null;
            ViewModelProperty.OnValueChanged = null;
        }

        /// <summary>
        /// 初始化View，当BindingContext改变时执行
        /// 此时还未绑定ViewModel
        /// </summary>
        protected virtual void OnInitialize()
        {
            //无所ViewModel的Value怎样变化，只对OnValueChanged事件监听(绑定)一次
            ViewModelProperty.OnValueChanged += OnBindingContextChanged;
        }

        /// <summary>
        /// 绑定的上下文发生改变时的响应方法
        /// 利用反射+=/-=OnValuePropertyChanged
        /// VM变化时，卸载老的 绑定新的
        /// </summary>
        public virtual void OnBindingContextChanged(T oldValue, T newValue)
        {
            Binder.Unbind(oldValue);
            Binder.Bind(newValue);
        }
    }
}
