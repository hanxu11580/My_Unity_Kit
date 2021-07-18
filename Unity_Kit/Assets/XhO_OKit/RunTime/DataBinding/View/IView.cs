using System;

namespace XhO_OKit.DataBinding
{
    /// <summary>
    /// 统一的视图接口
    /// </summary>
    public interface IView<T> where T : ViewModelBase
    {
        /// <summary>
        /// 数据逻辑VM
        /// </summary>
        T BindingContext { get; set; }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="immediate"></param>
        /// <param name="action"></param>
        //void Reveal();

        ///// <summary>
        ///// 隐藏
        ///// </summary>
        ///// <param name="immediate"></param>
        ///// <param name="action"></param>
        //void Hide();
    }
}
