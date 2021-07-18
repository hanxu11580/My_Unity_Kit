////
/// 这个感觉 有点问题
namespace XhO_OKit.BehaviorTree
{
    //修饰节点-直到子节点返回Failure
    //每一帧 子Node 都会去跑
    public class BTDecoratorFailure : BTComposite
    {
        public BTDecoratorFailure() : base()
        {
            mStrName = "DecoratorFailure";
        }

        public override void OnEnter(BTInput input) { }

        public override NodeResult OnExcute(BTInput input)
        {
            if (mListChildNode.Count <= 0)
                return NodeResult.SUCCESS;

            for (int i = 0; i < mListChildNode.Count; i++)
            {
                BTNode node = mListChildNode[i];
                NodeResult result = node.RunNode(input);
                if (NodeResult.FAILURE == result)
                    return NodeResult.SUCCESS;
            }
            return NodeResult.RUNNING;
        }

    }
}
