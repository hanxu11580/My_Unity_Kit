
namespace XhO_OKit.BehaviorTree
{
    /// <summary>
    /// 组合节点
    /// </summary>
    public abstract class BTComposite : BTNode
    {
        protected int mIRunIndex;

        public BTComposite() : base()
        {
            mStrName = "Composite";
        }

    }
}

