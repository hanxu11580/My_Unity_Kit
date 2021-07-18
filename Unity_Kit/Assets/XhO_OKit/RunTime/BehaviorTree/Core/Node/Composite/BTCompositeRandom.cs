
using UnityEngine;

namespace XhO_OKit.BehaviorTree
{
    /// <summary>
    /// 随机组合节点
    /// </summary>
    public class BTCompositeRandom : BTComposite
    {
        public BTCompositeRandom():base()
        {
            mStrName = "CompositeRandom";
        }

        public override void OnEnter(BTInput input)
        {
            if (mListChildNode.Count <= 0)
                mIRunIndex = -1;
            else
                mIRunIndex = Random.Range(0, mListChildNode.Count);
        }

        public override NodeResult OnExcute(BTInput input)
        {
            if (mIRunIndex < 0)
                return NodeResult.SUCCESS;

            return mListChildNode[mIRunIndex].RunNode(input);
        }
    }
}
