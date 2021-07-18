using System;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 有限状态机。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public sealed class Fsm<T> : FsmBase, IReference, IFsm<T> where T : class
    {
        readonly Dictionary<Type, FsmState<T>> m_States;
        private bool m_IsDestroyed;
        private float m_CurrentStateTime;
        /// <summary>
        /// 初始化有限状态机的新实例。
        /// </summary>
        public Fsm()
        {
            Owner = null;
            m_States = new Dictionary<Type, FsmState<T>>();
            CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        public T Owner { get; private set; }

        /// <summary>
        /// 获取有限状态机持有者类型。
        /// </summary>
        public override Type OwnerType => typeof(T);

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        public override int FsmStateCount => m_States.Count;

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        public override bool IsRunning => CurrentState != null;

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        public override bool IsDestroyed => m_IsDestroyed;

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        public FsmState<T> CurrentState { get; private set; }

        /// <summary>
        /// 获取当前有限状态机状态名称。
        /// </summary>
        public override string CurrentStateName => CurrentState?.GetType().FullName;

        public override float CurrentStateTime => m_CurrentStateTime;


        #region 创建Fsm

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<T> Create(string name, T owner, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                Debug.LogError("FSM owner is invalid.");
            }

            if (states == null || states.Length < 1)
            {
                Debug.LogError("FSM states is invalid.");
            }

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.Owner = owner;
            fsm.m_IsDestroyed = false;
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    Debug.LogError("FSM states is invalid.");
                }

                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    Debug.LogError(string.Format("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name).ToString(), stateType));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<T> Create(string name, T owner, List<FsmState<T>> states)
        {
            if (owner == null)
            {
                Debug.LogError("FSM owner is invalid.");
            }

            if (states == null || states.Count < 1)
            {
                Debug.LogError("FSM states is invalid.");
            }

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.Owner = owner;
            fsm.m_IsDestroyed = false;
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    Debug.LogError("FSM states is invalid.");
                }

                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    Debug.LogError(string.Format("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name).ToString(), stateType));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }

        #endregion

        #region 清除Fsm

        /// <summary>
        /// 清理有限状态机。
        /// </summary>
        public void ClearRef()
        {
            if (CurrentState != null)
            {
                CurrentState.OnLeave(this, true);
            }

            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                state.Value.OnDestroy(this);
            }

            Name = null;
            Owner = null;
            m_States.Clear();

            CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }
        #endregion

        #region 开始Fsm

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
        public void Start<TState>() where TState : FsmState<T>
        {
            Start(typeof(TState));
        }

        public void Start(Type type)
        {
            if (IsRunning)
            {
                Debug.LogError("FSM is running, can not start again.");
            }

            FsmState<T> state = GetState(type);
            if (state == null)
            {
                Debug.LogError(string.Format("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof(T), Name).ToString(), type.FullName));
            }

            m_CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }


        #endregion

        #region 是否存在状态

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState<TState>() where TState : FsmState<T>
        {
            return m_States.ContainsKey(typeof(TState));
        }

        #endregion


        #region 获取状态
        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        public TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if (m_States.TryGetValue(typeof(TState), out state))
            {
                return (TState)state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                Debug.LogError("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                Debug.LogError(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            if (m_States.TryGetValue(stateType, out FsmState<T> state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        public FsmState<T>[] GetAllStates()
        {
            int index = 0;
            FsmState<T>[] results = new FsmState<T>[m_States.Count];
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <param name="results">有限状态机的所有状态。</param>
        public void GetAllStates(List<FsmState<T>> results)
        {
            if (results == null)
            {
                Debug.LogError("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results.Add(state.Value);
            }
        }

        #endregion

        #region lifecycle

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        internal override void FixedUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (CurrentState == null) return;
            CurrentState.OnFixedUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        internal override void Shutdown()
        {
            ReferencePool.Release(this);
        }
        #endregion

        #region 切换Fsm状态

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        public void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        public void ChangeState(Type stateType)
        {
            if (CurrentState == null)
            {
                Debug.LogError("Current state is invalid.");
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                Debug.LogError(string.Format("FSM '{0}' can not change state to '{1}' which is not exist.", new TypeNamePair(typeof(T), Name).ToString(), stateType.FullName));
            }

            CurrentState.OnLeave(this, false);
            m_CurrentStateTime = 0f;
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        #endregion
    }
}
