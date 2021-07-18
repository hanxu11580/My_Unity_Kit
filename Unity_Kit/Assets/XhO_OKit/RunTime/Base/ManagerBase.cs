namespace XhO_OKit
{
    public abstract class ManagerBase
    {
        /// <summary> 优先级 </summary>
        public virtual int Priority => 0;

        /// <summary> 模块初始化 ，替代构造函数</summary>
        internal abstract void InitModule();

        /// <summary> 模块循环 </summary>
        internal abstract void UpdateModule(float elapseSeconds, float realElapseSeconds);

        internal virtual void FixedUpdateModule(float elapseSeconds, float realElapseSeconds) { }

        /// <summary> 模块关闭 </summary>
        internal abstract void ShutDownModule();
    }
}
