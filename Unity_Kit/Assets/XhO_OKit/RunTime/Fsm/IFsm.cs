using System;
using System.Collections.Generic;

namespace XhO_OKit
{
    /// <summary>
    /// 有限状态机接口。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public interface IFsm<T> where T : class
    {
        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取有限状态机完整名称。
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        T Owner { get; }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        int FsmStateCount { get; }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        bool IsDestroyed { get; }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        FsmState<T> CurrentState { get; }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        float CurrentStateTime { get; }

        #region Fsm可执行操作
        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
        void Start<TState>() where TState : FsmState<T>;

        void Start(Type type);

        /// <summary>
        /// 有限状态机是否有TState状态
        /// </summary>
        /// <typeparam name="TState">状态类型</typeparam>
        /// <returns></returns>
        bool HasState<TState>() where TState : FsmState<T>;

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        TState GetState<TState>() where TState : FsmState<T>;

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        FsmState<T> GetState(Type stateType);

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        FsmState<T>[] GetAllStates();

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <param name="results">有限状态机的所有状态。</param>
        void GetAllStates(List<FsmState<T>> results);

        #endregion
    }
}
