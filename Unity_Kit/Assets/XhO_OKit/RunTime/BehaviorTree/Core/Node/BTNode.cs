
using System.Collections.Generic;

namespace XhO_OKit.BehaviorTree
{
    public abstract class BTNode
    {
        /// <summary> 命名空间下全名 </summary>
        protected string mStrType = string.Empty;

        /// <summary> 类名 </summary>
        protected string mStrName = string.Empty;

        /// <summary> 父节点node </summary>
        protected BTNode mParentNode = null;

        /// <summary> 子节点集合 </summary>
        protected List<BTNode> mListChildNode = new List<BTNode>();

        /// <summary> 向父节点返回结果 </summary>
        private NodeResult mResultState = NodeResult.NONE;

        public BTNode()
        {
            mStrType = GetType().FullName;
            mStrName = GetType().Name;
        }

        /// <summary>
        /// 进入
        /// </summary>
        public virtual void OnEnter(BTInput input) { }

        /// <summary>
        /// 执行
        /// </summary>
        public virtual NodeResult OnExcute(BTInput input)
        {
            return NodeResult.SUCCESS;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="input"></param>
        public virtual void OnExit(BTInput input) { }

        /// <summary>
        /// 添加子节点
        /// </summary>
        public BTNode AddChild(BTNode node)
        {
            if (!mListChildNode.Contains(node))
                mListChildNode.Add(node);
            return this;
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        public void RemoveChild(BTNode node)
        {
            if (mListChildNode.Contains(node))
                mListChildNode.Remove(node);
        }

        /// <summary>
        /// 运行节点
        /// </summary>
        public NodeResult RunNode(BTInput input)
        {
            //每次跑这个节点 先看看是否是None
            if (mResultState == NodeResult.NONE)
            {
                OnEnter(input);
                mResultState = NodeResult.RUNNING;
            }

            NodeResult result = OnExcute(input);
            
            //每次跑完 看看是否执行在Running，不是的话重新设为None
            if (result != NodeResult.RUNNING)
            {
                OnExit(input);
                mResultState = NodeResult.NONE;
            }
            return result;
        }

        public void ResetNode()
        {
            mResultState = NodeResult.NONE;
            if (mListChildNode.Count > 0)
            {
                foreach (var node in mListChildNode)
                {
                    node.ResetNode();
                }   
            }
        }
        
    }
}
