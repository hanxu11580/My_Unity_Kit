using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 表格长什么样子,必须以id打头
    /// id   desc
    /// int  string
    /// 001  一只狗
    /// </summary>
    public class ConfigManager : ManagerBase
    {
        public Hashtable configs = new Hashtable();

        #region ManagerBase

        internal override void InitModule()
        {
            foreach (Type item in FrameworkEntry.types.Values)
            {
                if (item.IsAbstract) continue;
                ConfigAttribute[] configAttributes = (ConfigAttribute[])item.GetCustomAttributes(typeof(ConfigAttribute), false);
                if (configAttributes.Length > 0)
                {
                    IConfigTable iConfigTable = (IConfigTable)Activator.CreateInstance(item);
                    //初始化数据表
                    iConfigTable.InitConfigTable();
                    configs.Add(iConfigTable.ConfigType, iConfigTable);
                }
            }
        }

        internal override void UpdateModule(float elapseSeconds, float realElapseSeconds){}

        internal override void ShutDownModule()
        {
            configs.Clear();
        }
        
        #endregion

        public T GetConfig<T>(int id) where T : IConfig
        {
            Type type = typeof(T);
            if (configs.ContainsKey(type))
            {
                ConfigTable<T> ConfigTable = (ConfigTable<T>)configs[type];
                return ConfigTable.GetConfig(id);
            }
            else
            {
                KitLog.Log($"Config不存在{type.Name}");
                return default;
            }
        }
        public Dictionary<int, T> GetConfigs<T>() where T : IConfig
        {
            Type type = typeof(T);
            if (configs.ContainsKey(type))
            {
                ConfigTable<T> ConfigTable = (ConfigTable<T>)configs[type];
                return ConfigTable.GetConfigs();
            }
            else
            {
                KitLog.Log($"Config不存在{type.Name}");
                return default;
            }
        }
    }
}