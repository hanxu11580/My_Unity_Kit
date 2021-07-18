
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{

    public static partial class CommonTools
    {

        /// <summary>
        /// 随机取数组中某个
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T GetRandomObject<T>(T[] arr)
        {
            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }
        /// <summary>
        /// 随机取List中某个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static T GetRandomObject<T>(List<T> lists)
        {
            return lists[UnityEngine.Random.Range(0, lists.Count)];
        }
        /// <summary>
        /// 延迟调用
        /// </summary>
        /// <param name="runTask"></param>
        /// <param name="delayTime"></param>
        /// <returns></returns>
        public static IEnumerator RunDelay(Action runTask, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            runTask?.Invoke();
            
        }
        /// <summary>
        /// 角度转单位向量
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 DirFromAngle(float angle)
        {
            //以x为起始轴
            //return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad));
            //以z为起始轴
            //angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        }

        /// <summary>
        /// 粗略判断是否在屏幕中
        /// </summary>
        /// <returns></returns>
        public static bool IsInScreen(Camera camera,Vector3 worldPos)
        {
            Vector3 portPos = camera.WorldToViewportPoint(worldPos);
            if(portPos.x > 0 && portPos.x < 1 && portPos.y > 0 && portPos.y < 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 屏幕转世界
        /// </summary>
        /// <param name="target"></param>
        /// <param name="screenPos"></param>
        /// <param name="renderCamera"></param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Transform target, Vector3 screenPos, Camera renderCamera)
        {
            Vector3 camera2TargetVector = target.position - renderCamera.transform.position;
            float depth = Vector3.Dot(camera2TargetVector, renderCamera.transform.forward);
            screenPos.z = depth;
            return renderCamera.ScreenToWorldPoint(screenPos);
        }

        /// <summary>
        /// 在xz平面拖拽物体
        /// </summary>
        /// <param name="target"></param>
        /// <param name="screenPos"></param>
        /// <param name="renderCamera"></param>
        /// <param name="limitHei"></param>
        /// <returns></returns>
        public static Vector3 DragToXZPlane(Transform target, Vector3 screenPos, Camera renderCamera, float limitHei)
        {
            Vector3 pos = ScreenToWorldPos(target, screenPos, renderCamera);
            Vector3 camera2TargetVector = (target.position - renderCamera.transform.position).normalized;
            // 限制高度就是限制y分量。 根据高度差和camera2TargetVector(单位向量)的y分量得到向量整体比例系数
            // 可以得到如何将原Pos 便宜到camera2TargetVector方向上
            float percent = (limitHei - pos.y) / camera2TargetVector.y;
            pos += camera2TargetVector * percent;
            return pos;
        }

        /// <summary>
        /// 在Xz移动，屏幕滑动多少 移动多少
        /// </summary>
        /// <param name="target"></param>
        /// <param name="screenOffset"></param>
        /// <param name="renderCamera"></param>
        /// <param name="limitHei"></param>
        /// <returns></returns>
        public static Vector3 FollowMouseOnXzMovement(Transform target, Vector3 screenOffset, Camera renderCamera, float limitHei)
        {
            Vector3 targetScreen = renderCamera.WorldToScreenPoint(target.position);
            Vector3 currScreen = targetScreen + screenOffset;
            return DragToXZPlane(target, currScreen, renderCamera, limitHei);
        }

        /// <summary>
        /// 在Xy移动，屏幕滑动多少 移动多少
        /// </summary>
        public static Vector3 FollowMouseOnXyMovement(Transform target, Vector3 screenOffset, Camera renderCamera)
        {
            Vector3 targetScreen = renderCamera.WorldToScreenPoint(target.position);
            Vector3 currScreen = targetScreen + screenOffset;
            return ScreenToWorldPos(target, currScreen, renderCamera);
        }


        #region 有问题的东西

        /// <summary>
        /// 世界坐标转Ugui坐标 有问题！！！！！！！！！！！
        /// </summary>
        /// <param name = "worldPos" ></ param >
        /// < returns ></ returns >
        //public static Vector2 World2UguiPos(Vector3 worldPos)
        //{
        //    Vector3 scrPos = Camera.main.WorldToScreenPoint(worldPos);
        //    scrPos.z = 0f;
        //    //转UGui坐标
        //    //得到屏幕中心坐标
        //    Vector3 scrCenterPos = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        //    var dir = scrPos - scrCenterPos;
        //    Vector2 v2 = new Vector2(dir.x, dir.y);
        //    Debug.Log(v2);
        //    return v2;
        //}        

        #endregion


    }
}
