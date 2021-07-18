using System;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 有限状态机状态基类。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型</typeparam>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// 初始化有限状态机状态基类的新实例。
        /// </summary>
        public FsmState() { }


        #region lifecycle
        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        public virtual void OnInit(IFsm<T> fsm) { }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        public virtual void OnEnter(IFsm<T> fsm) { }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        public virtual void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds) { }

        /// <summary>
        /// 物理行为轮询
        /// </summary>
        public virtual void OnFixedUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds) { }

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        public virtual void OnLeave(IFsm<T> fsm, bool isShutdown) { }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        public virtual void OnDestroy(IFsm<T> fsm) { }

        #endregion

        #region 状态执行时可进行操作
        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        /// <param name="fsm">有限状态机引用。</param>
        public void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                Debug.LogError("FSM is invalid.");
            }

            fsmImplement.ChangeState<TState>();
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        protected void ChangeState(IFsm<T> fsm, Type stateType)
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                Debug.LogError("FSM is invalid.");
            }

            if (stateType == null)
            {
                Debug.LogError("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                Debug.LogError(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            fsmImplement.ChangeState(stateType);
        }

        #endregion
    }
}
