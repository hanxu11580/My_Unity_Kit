using System;

namespace XhO_OKit
{
    /// <summary>
    /// 数据表
    /// </summary>
    public interface IConfigTable
    {
        /// <summary>
        /// 数据表内每一行类型
        /// </summary>
        Type ConfigType
        {
            get;
        }
        void InitConfigTable();
    }
}