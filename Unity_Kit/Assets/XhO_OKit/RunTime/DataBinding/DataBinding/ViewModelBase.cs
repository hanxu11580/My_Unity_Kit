namespace XhO_OKit.DataBinding
{
    public class ViewModelBase
    {
        public ViewModelBase ParentViewModel { get; set; }

        /// <summary>
        /// 第一次显示前 初始化
        /// </summary>
        public virtual void OnInitialize()
        {
            
        }

        public virtual void OnDestory()
        {
            
        }

    }
}
