
using System;

namespace XhO_OKit.DataNode
{
    /// <summary>
    /// 数据结点管理器。
    /// 主要以树形结构存储游戏运行时数据
    /// </summary>
    public sealed partial class DataNodeManager : ManagerBase, IDataNodeManager
    {
        //缓存的空数组及固定字符串
        private static readonly string[] EmptyStringArray = new string[] { };
        private const string RootName = "<Root>";
        private static readonly string[] PathSplitSeparator = new string[] { ".", "/", "\\" };
        
        private DataNode m_Root;

        /// <summary>
        /// 获取根数据结点。
        /// </summary>
        public IDataNode Root => m_Root;

        #region 模块lifecycle
        
            internal override void InitModule()
            {
                m_Root = DataNode.Create(RootName, null);
            }
            /// <summary>
            /// 数据结点管理器轮询。
            /// </summary>
            /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
            /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
            internal override void UpdateModule(float elapseSeconds, float realElapseSeconds){}

            internal override void FixedUpdateModule(float elapseSeconds, float realElapseSeconds){}

            /// <summary>
            /// 关闭并清理数据结点管理器。
            /// </summary>
            internal override void ShutDownModule()
            {
                ReferencePool.Release(m_Root);
                m_Root = null;
            }
        
        #endregion
        
        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path) where T : Variable
        {
            return GetData<T>(path, null);
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path)
        {
            return GetData(path, null);
        }

        /// <summary>
        /// 根据类型获取数据结点的数据。
        /// </summary>
        /// <typeparam name="T">要获取的数据类型。</typeparam>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定类型的数据。</returns>
        public T GetData<T>(string path, IDataNode node) where T : Variable
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new XhO_OKitException(StringExtension.Format(
                    "Data node is not exist, path '{0}', node '{1}'.", path,
                    node != null ? node.FullName : string.Empty));
            }

            return current.GetData<T>();
        }

        /// <summary>
        /// 获取数据结点的数据。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>数据结点的数据。</returns>
        public Variable GetData(string path, IDataNode node)
        {
            IDataNode current = GetNode(path, node);
            if (current == null)
            {
                throw new XhO_OKitException(StringExtension.Format(
                    "Data node is not exist, path '{0}', node '{1}'.", path,
                    node != null ? node.FullName : string.Empty));
            }

            return current.GetData();
        }

        #region 设置数据，如果没有会自动创建Node

            /// <summary>
            /// 设置数据结点的数据。
            /// </summary>
            /// <typeparam name="T">要设置的数据类型。</typeparam>
            /// <param name="path">相对于 node 的查找路径。</param>
            /// <param name="data">要设置的数据。</param>
            public void SetData<T>(string path, T data) where T : Variable
            {
                SetData(path, data, null);
            }

            /// <summary>
            /// 设置数据结点的数据。
            /// </summary>
            /// <param name="path">相对于 node 的查找路径。</param>
            /// <param name="data">要设置的数据。</param>
            public void SetData(string path, Variable data)
            {
                SetData(path, data, null);
            }

            /// <summary>
            /// 设置数据结点的数据。
            /// </summary>
            /// <typeparam name="T">要设置的数据类型。</typeparam>
            /// <param name="path">相对于 node 的查找路径。</param>
            /// <param name="data">要设置的数据。</param>
            /// <param name="node">查找起始结点。</param>
            public void SetData<T>(string path, T data, IDataNode node) where T : Variable
            {
                IDataNode current = GetOrAddNode(path, node);
                current.SetData(data);
            }

            /// <summary>
            /// 设置数据结点的数据。
            /// </summary>
            /// <param name="path">相对于 node 的查找路径。</param>
            /// <param name="data">要设置的数据。</param>
            /// <param name="node">查找起始结点。</param>
            public void SetData(string path, Variable data, IDataNode node)
            {
                IDataNode current = GetOrAddNode(path, node);
                current.SetData(data);
            }
            
        #endregion
        
        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path)
        {
            return GetNode(path, null);
        }

        /// <summary>
        /// 获取数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。为null 则从root查找</param>
        /// <returns>指定位置的数据结点，如果没有找到，则返回空。</returns>
        public IDataNode GetNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetChild(i);
                //其中任何一个节点为null 返回null
                if (current == null)
                {
                    return null;
                }
            }

            return current;
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则创建相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path)
        {
            return GetOrAddNode(path, null);
        }

        /// <summary>
        /// 获取或增加数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        /// <returns>指定位置的数据结点，如果没有找到，则增加相应的数据结点。</returns>
        public IDataNode GetOrAddNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                current = current.GetOrAddChild(i);
            }

            return current;
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        public void RemoveNode(string path)
        {
            RemoveNode(path, null);
        }

        /// <summary>
        /// 移除数据结点。
        /// </summary>
        /// <param name="path">相对于 node 的查找路径。</param>
        /// <param name="node">查找起始结点。</param>
        public void RemoveNode(string path, IDataNode node)
        {
            IDataNode current = node ?? m_Root;
            IDataNode parent = current.Parent;
            string[] splitedPath = GetSplitedPath(path);
            foreach (string i in splitedPath)
            {
                parent = current;
                current = current.GetChild(i);
                if (current == null)
                {
                    return;
                }
            }
            //循环完 parent将是删除节点的父节点，利用父节点方法删除对应的Node
            if (parent != null)
            {
                parent.RemoveChild(current.Name);
            }
        }

        /// <summary>
        /// 移除所有数据结点。
        /// </summary>
        public void Clear()
        {
            m_Root.Clear();
        }

        /// <summary>
        /// 数据结点路径切分工具函数。
        /// </summary>
        /// <param name="path">要切分的数据结点路径。</param>
        /// <returns>切分后的字符串数组。</returns>
        private static string[] GetSplitedPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return EmptyStringArray;
            }
            
            //StringSplitOptions.RemoveEmptyEntries去除空数据
            return path.Split(PathSplitSeparator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
