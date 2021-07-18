
namespace XhO_OKit.BehaviorTree
{
    /// <summary>
    /// 顺序组合节点
    /// 一个Node返回Success那么执行后续Node，如果一个Node返回Failure那么将会返回Failure
    /// </summary>
    public class BTCompositeSequence : BTComposite
    {
        public BTCompositeSequence():base()
        {
            mStrName = "CompositeSequence";
        }

        public override void OnEnter(BTInput input)
        {
            mIRunIndex = 0;
        }

        public override NodeResult OnExcute(BTInput input)
        {
            if (mListChildNode.Count <= 0)
                return NodeResult.SUCCESS;
            if (mIRunIndex >= mListChildNode.Count) //全部成功跑完
                return NodeResult.SUCCESS;

            BTNode node = mListChildNode[mIRunIndex];
            NodeResult result = node.RunNode(input);

            if (NodeResult.FAILURE == result)
                return NodeResult.FAILURE;

            if (NodeResult.SUCCESS == result)
                mIRunIndex++;

            return NodeResult.RUNNING;
        }
       
    }
}
