using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace XhO_OKit
{
    public abstract class ConfigTable<T> : IConfigTable where T : IConfig
    {
        public Dictionary<int, T> configDict = new Dictionary<int, T>();
        public Type ConfigType
        {
            get
            {
                return typeof(T);
            }
        }
        
        /// <summary>
        /// 初始化数据表
        /// </summary>
        public void InitConfigTable()
        {
            TextAsset asset = AssetManager.Instance.LoadAsset<TextAsset>(typeof(T).Name, ".txt", false, true, "Config");

            foreach (KeyValuePair<string, T> item in JsonMapper.ToObject<Dictionary<string, T>>(asset.text))
            {
                configDict.Add(int.Parse(item.Key), item.Value);
            }
        }
        public T GetConfig(int id)
        {
            if (configDict.ContainsKey(id))
            {
                return configDict[id];
            }
            else
            {
                return default;
            }
        }
        public Dictionary<int, T> GetConfigs()
        {
            return configDict;
        }
    }
}