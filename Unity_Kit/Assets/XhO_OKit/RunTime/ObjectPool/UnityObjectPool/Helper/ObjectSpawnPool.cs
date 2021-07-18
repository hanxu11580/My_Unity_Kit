using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 对象池
    /// </summary>
    public sealed class ObjectSpawnPool
    {
        private GameObject _spawnTem;
        private int _limit;
        private Queue<GameObject> _objectQueue = new Queue<GameObject>();
        private XhO_OKitAction<GameObject> _onSpawn;
        private XhO_OKitAction<GameObject> _onDespawn;

        public ObjectSpawnPool(GameObject spawnTem, int limit, XhO_OKitAction<GameObject> onSpawn, XhO_OKitAction<GameObject> onDespawn)
        {
            _spawnTem = spawnTem;
            _limit = limit;
            _onSpawn = onSpawn;
            _onDespawn = onDespawn;
        }

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count
        {
            get
            {
                return _objectQueue.Count;
            }
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        /// <returns>对象</returns>
        public GameObject Spawn()
        {
            GameObject obj;
            if (_objectQueue.Count > 0)
            {
                obj = _objectQueue.Dequeue();
            }
            else
            {
                obj = GameObject.Instantiate(_spawnTem);
            }

            obj.SetActive(true);

            _onSpawn?.Invoke(obj);

            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">对象</param>
        public void Despawn(GameObject obj)
        {
            if (_objectQueue.Count >= _limit)
            { //大于限制数量
                _onDespawn?.Invoke(obj);

                GameObject.Destroy(obj);
            }
            else
            {
                obj.SetActive(false);

                _onDespawn?.Invoke(obj);

                _objectQueue.Enqueue(obj);
            }
        }

        /// <summary>
        /// 清空所有对象
        /// </summary>
        public void Clear()
        {
            while (_objectQueue.Count > 0)
            {
                GameObject obj = _objectQueue.Dequeue();
                if (obj)
                {
                    GameObject.Destroy(obj);
                }
            }
        }
    }
}