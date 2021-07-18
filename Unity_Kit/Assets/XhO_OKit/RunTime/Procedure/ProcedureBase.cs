using ProcedureOwner = XhO_OKit.IFsm<XhO_OKit.IProcedureManager>;

namespace XhO_OKit
{
    /// <summary>
    /// 流程就是一个状态
    /// 流程基类(是一种状态,且持有者为ProcedureManager)
    /// </summary>
    public class ProcedureBase:FsmState<IProcedureManager>
    {
        /// <summary>
        /// 流程初始化
        /// </summary>
        public override void OnInit(ProcedureOwner fsm)
        {
            base.OnInit(fsm);
        }

        /// <summary>
        /// 流程进入
        /// </summary>
        public override void OnEnter(ProcedureOwner fsm)
        {
            base.OnEnter(fsm);
        }

        /// <summary>
        /// 流程离开
        /// </summary>
        public override void OnLeave(ProcedureOwner fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        /// <summary>
        /// 流程轮询时
        /// </summary>
        public override void OnUpdate(ProcedureOwner fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 销毁时
        /// </summary>
        public override void OnDestroy(ProcedureOwner fsm)
        {
            base.OnDestroy(fsm);
        }
    }
}
