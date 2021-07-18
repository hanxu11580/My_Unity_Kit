using System;
using UnityEngine;


namespace XhO_OKit
{
    /// <summary>
    /// 流程管理器(流程Fsm二次封装)
    /// </summary>
    public class ProcedureManager : ManagerBase,IProcedureManager
    {
        /// <summary>
        /// 状态机管理器
        /// </summary>
        private IFsmManager m_FsmManager;

        /// <summary>
        /// 流程的状态机
        /// </summary>
        private IFsm<IProcedureManager> m_ProcedureFsm;

        /// <summary>
        /// 当前流程
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                ValidityCheck();
                return (ProcedureBase)m_ProcedureFsm.CurrentState;
            }
        }

        #region interface实现流程操作
        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">有限状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        public void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures)
        {
            if(fsmManager == null)
            {
                Debug.LogError("FSM manager is invalid.");
            }
            m_FsmManager = fsmManager;
            //利用管理器创建，                        |
            //                                        V
            //                             (this)流程管理器就是状态机的持有者
            m_ProcedureFsm = m_FsmManager.CreateFsm(this, procedures);
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        public void StartProcedure<T>() where T : ProcedureBase
        {
            ValidityCheck();
            m_ProcedureFsm.Start<T>();
        }

        public void StartProcedure(Type procedure)
        {
            ValidityCheck();
            m_ProcedureFsm.Start(procedure);
        }


        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            ValidityCheck();
            return m_ProcedureFsm.HasState<T>();
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            ValidityCheck();
            //ProcedureBase也是状态
            return m_ProcedureFsm.GetState<T>();
        }

        #endregion

        #region 基类实现

        /// <summary>
        /// 为什么放这？
        /// 入口创建这个管理器实例时，将会调用。也算是基类的必需品
        /// </summary>
        public ProcedureManager()
        {
            m_FsmManager = null;
            m_ProcedureFsm = null;
        }


        public override int Priority => 10;

        internal override void InitModule()
        {
            //这样写不太方便,需要向m_procedures添加流程状态，然后设置入口，然后再开始
            //现在直接初始化，然后选择开始其中一个就行了
            //m_FsmManager = FrameworkEntry.Instance.GetManager<FsmManager>();
            //m_ProcedureFsm = null;
            //m_procedures = new List<ProcedureBase>();
        }
        /// <summary>
        /// 不用实现,在Fsm在跑
        /// </summary>
        internal override void UpdateModule(float elapseSeconds, float realElapseSeconds) { }

        internal override void ShutDownModule()
        {
            if (m_FsmManager != null)
            {
                if (m_ProcedureFsm != null)
                {
                    //利用状态机管理器删除 流程状态机
                    m_FsmManager.DestroyFsm(m_ProcedureFsm);
                    m_ProcedureFsm = null;
                }
                m_FsmManager = null;
            }
        }
        #endregion

        #region Private

        /// <summary>
        /// 重复代码合法性检查
        /// </summary>
        void ValidityCheck()
        {
            if (m_ProcedureFsm == null)
            {
                Debug.LogError("You must initialize procedure first.");
            }
        }

        #endregion
    }
}
