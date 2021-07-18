using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XhO_OKit
{
    /// <summary>
    /// GUI
    /// </summary>
    public partial class CommonTools
    {
        /// <summary>
        /// UI射线检测
        /// </summary>
        /// <param name="eventSys"></param>
        /// <param name="graphicRaycaster"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public static bool GuiRaycastObjects(EventSystem eventSys, GraphicRaycaster graphicRaycaster, List<RaycastResult> results)
        {
            results.Clear();
            PointerEventData eventData = new PointerEventData(eventSys);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
            graphicRaycaster.Raycast(eventData, results);
            int count = results.Count;
            return count > 0;
        }



        /// <summary>
        /// UI穿透判断
        /// </summary>
        /// <param name="eventSys">Canvas创建时的EventSystem</param>
        /// <param name="graphicRaycaster">Canvas下的组件</param>
        /// <returns></returns>
        public static bool CheckGuiRaycastObjects(EventSystem eventSys, GraphicRaycaster graphicRaycaster)
        {
            //防止频繁new List
            List<RaycastResult> results = ListPool<RaycastResult>.Allocate();
            GuiRaycastObjects(eventSys, graphicRaycaster, results);
            return results.Count > 0;
        }
    }
}
