using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XhO_OKit.DataNode;

namespace XhO_OKit
{
    /// <summary>
    /// 模块入口
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class FrameworkEntry : SingletonMono<FrameworkEntry>
    {
        private readonly static LinkedList<ManagerBase> m_Managers = new LinkedList<ManagerBase>();
        public readonly static Hashtable types = new Hashtable();

        private void Start()
        {
            GetAssemblyAllTypes();
        }

        #region PUBLIC
        /// <summary>
        /// 获取对应管理器
        /// </summary>
        /// <typeparam name="T">ManagerBase派生类</typeparam>
        /// <returns></returns>
        public static T GetManager<T>() where T : ManagerBase
        {
            Type managerType = typeof(T);
            foreach (ManagerBase manager in m_Managers)
            {
                if (manager.GetType() == managerType)
                {
                    return manager as T;
                }
            }
            return CreateManager(managerType) as T;
        }

        #endregion

        #region lifecycle

        void Update()
        {
            foreach (ManagerBase manager in m_Managers)
            {
                manager.UpdateModule(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }

        void FixedUpdate()
        {
            foreach (ManagerBase manager in m_Managers)
            {
                manager.FixedUpdateModule(Time.fixedDeltaTime, Time.unscaledDeltaTime);
            }
        }

        protected override void OnDestroy()
        {
            for (LinkedListNode<ManagerBase> current = m_Managers.Last; current != null; current = current.Previous)
            {
                current.Value.ShutDownModule();
            }
            m_Managers.Clear();
        }

        #endregion

        #region Private

        private void GetAssemblyAllTypes()
        {

            foreach (Type item in GetType().Assembly.GetTypes())
            {
                if (!types.ContainsKey(item.Name))
                {
                    types.Add(item.Name, item);
                }
            }
        }


        /// <summary>
        /// 创建模块实例
        /// </summary>
        /// <param name="managerType">模块类型</param>
        /// <returns></returns>
        static ManagerBase CreateManager(Type managerType)
        {
            ManagerBase manager = Activator.CreateInstance(managerType) as ManagerBase;
            if (manager == null)
            {
                Debug.LogError("模块" + manager.GetType().FullName + "创建失败");
            }

            //根据优先级放入链表
            LinkedListNode<ManagerBase> current = m_Managers.First;
            while (current != null)
            {
                if (manager.Priority < current.Value.Priority)
                {
                    break;
                }
                current = current.Next;
            }

            if (current != null)
            {
                m_Managers.AddBefore(current, manager);
            }
            else
            {
                m_Managers.AddLast(manager);
            }

            manager.InitModule();
            return manager;
        }

        #endregion
    }
}
