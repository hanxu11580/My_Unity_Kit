
namespace XhO_OKit.BehaviorTree
{
    /// <summary>
    /// 并行组合节点
    /// 并行执行子节点，如果任何子节点返回Failure则中止，直到全部返回Success
    /// </summary>
    public class BTCompositeParlallel : BTComposite
    {
        public BTCompositeParlallel() : base()
        {
            mStrName = "CompositeParlallel";
        }

        public override void OnEnter(BTInput input)
        {
            mIRunIndex = 0;
        }

        public override NodeResult OnExcute(BTInput input)
        {
            //空并行Node直接返回Success，或遍历子Node索引大于子Node数量,直接返回Success
            if (mListChildNode.Count <= 0)
                return NodeResult.SUCCESS;
            if (mIRunIndex >= mListChildNode.Count)
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
