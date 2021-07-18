
namespace XhO_OKit.BehaviorTree
{
    
    /// <summary>
    /// 选择组合节点
    /// 直到有一个Node返回Success，或者所有Node返回Failure
    /// </summary>
    public class BTCompositeSelector : BTComposite
    {
        public BTCompositeSelector() : base()
        {
            mStrName = "CompositeSelector";
        }

        public override void OnEnter(BTInput input)
        {
            mIRunIndex = 0;
        }

        public override NodeResult OnExcute(BTInput input)
        {
            if (mListChildNode.Count <= 0)
                return NodeResult.SUCCESS;
            if (mIRunIndex >= mListChildNode.Count) //跑完了,返回Failure
                return NodeResult.FAILURE;

            BTNode node = mListChildNode[mIRunIndex];
            NodeResult result = node.RunNode(input);

            if (NodeResult.SUCCESS == result)
                return NodeResult.SUCCESS;

            if (NodeResult.FAILURE == result)
                mIRunIndex++;

            return NodeResult.RUNNING;
        }
    }
}
