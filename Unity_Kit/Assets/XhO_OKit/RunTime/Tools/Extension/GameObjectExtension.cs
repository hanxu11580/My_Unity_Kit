using System;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 显示
        /// </summary>
        public static void Visible(this GameObject go, bool isShow)
        {
            if (go.activeSelf != isShow)
            { //多了条判断 避免重复设置SetActive
                go.SetActive(isShow);
            }
        }

        /// <summary>
        /// MonoBehavior控制显示
        /// </summary>
        public static void Visible(this MonoBehaviour monoBehaviour, bool isShow)
        {
            monoBehaviour.gameObject.Visible(isShow);
        }



        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="go"></param>
        public static void Destroy(this GameObject go)
        {
            GameObject.Destroy(go);
        }
        /// <summary>
        /// 延迟销毁
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delayTime"></param>
        public static void DestroyDelay(this GameObject go, float delayTime)
        {
            GameObject.Destroy(go, delayTime);
        }

        /// <summary>
        /// 设置自身及所有子物体的层
        /// </summary>
        /// <param name="gameObject">自身</param>
        /// <param name="layer">层</param>
        public static void SetLayerIncludeChildren(this GameObject go, int layer)
        {
            foreach (Transform tran in go.GetComponentsInChildren<Transform>(true))
            {
                tran.gameObject.layer = layer;
            }
        }
    }
}
