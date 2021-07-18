﻿using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 默认的对象池管理器助手
    /// </summary>
    public sealed class DefaultObjectPoolHelper : IObjectPoolHelper
    {
        /// <summary>
        /// 所有对象池
        /// </summary>
        public Dictionary<string, ObjectSpawnPool> SpawnPools { get; } = new Dictionary<string, ObjectSpawnPool>();

        /// <summary>
        /// 对象池默认上限
        /// </summary>
        private int _limit = 50;

        /// <summary>
        /// 注册对象池
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <param name="spawnTem">对象模板</param>
        /// <param name="onSpawn">对象生成时初始化委托</param>
        /// <param name="onDespawn">对象回收时处理委托</param>
        /// <param name="limit">对象池上限，等于0时，表示使用默认值</param>
        public void RegisterSpawnPool(string name, GameObject spawnTem, XhO_OKitAction<GameObject> onSpawn, XhO_OKitAction<GameObject> onDespawn, int limit)
        {
            if (!SpawnPools.ContainsKey(name))
            {
                SpawnPools.Add(name, new ObjectSpawnPool(spawnTem, limit <= 0 ? _limit : limit, onSpawn, onDespawn));
            }
            else
            {
                throw new XhO_OKitException("注册对象池失败：已存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 是否存在指定名称的对象池
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistSpawnPool(string name)
        {
            return SpawnPools.ContainsKey(name);
        }
        /// <summary>
        /// 移除已注册的对象池
        /// </summary>
        /// <param name="name">对象池名称</param>
        public void UnRegisterSpawnPool(string name)
        {
            if (SpawnPools.ContainsKey(name))
            {
                SpawnPools[name].Clear();
                SpawnPools.Remove(name);
            }
            else
            {
                throw new XhO_OKitException("移除对象池失败：不存在对象池 " + name + " ！");
            }
        }

        /// <summary>
        /// 获取对象池中对象数量
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <returns>对象数量</returns>
        public int GetPoolCount(string name)
        {
            if (SpawnPools.ContainsKey(name))
            {
                return SpawnPools[name].Count;
            }
            else
            {
                throw new XhO_OKitException("获取对象数量失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <returns>对象</returns>
        public GameObject Spawn(string name)
        {
            if (SpawnPools.ContainsKey(name))
            {
                return SpawnPools[name].Spawn();
            }
            else
            {
                throw new XhO_OKitException("生成对象失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <param name="target">对象</param>
        public void Despawn(string name, GameObject target)
        {
            if (SpawnPools.ContainsKey(name))
            {
                SpawnPools[name].Despawn(target);
            }
            else
            {
                throw new XhO_OKitException("回收对象失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 批量回收对象
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <param name="targets">对象数组</param>
        public void Despawns(string name, GameObject[] targets)
        {
            if (SpawnPools.ContainsKey(name))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    SpawnPools[name].Despawn(targets[i]);
                }
            }
            else
            {
                throw new XhO_OKitException("回收对象失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 批量回收对象
        /// </summary>
        /// <param name="name">对象池名称</param>
        /// <param name="targets">对象集合</param>
        public void Despawns(string name, List<GameObject> targets)
        {
            if (SpawnPools.ContainsKey(name))
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    SpawnPools[name].Despawn(targets[i]);
                }
                targets.Clear();
            }
            else
            {
                throw new XhO_OKitException("回收对象失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 清空指定的对象池
        /// </summary>
        /// <param name="name">对象池名称</param>
        public void Clear(string name)
        {
            if (SpawnPools.ContainsKey(name))
            {
                SpawnPools[name].Clear();
            }
            else
            {
                throw new XhO_OKitException("清空对象池失败：不存在对象池 " + name + " ！");
            }
        }
        /// <summary>
        /// 清空所有对象池
        /// </summary>
        public void ClearAll()
        {
            foreach (var spawnPool in SpawnPools)
            {
                spawnPool.Value.Clear();
            }
        }
    }
}