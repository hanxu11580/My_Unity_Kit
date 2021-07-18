
using UnityEngine;


namespace XhO_OKit
{
    /// <summary>
    /// 服务是可以配置的
    /// 射线检测服务
    /// </summary>
    [CreateAssetMenu(menuName = "XhO_OKit/CreateRaycastService")]
    public class RaycastService : ScriptableObject
    {
        [SerializeField]
        LayerMask layerMaskConfig;

        #region 2D

        public RaycastHit2D Raycast2D(Vector2 origin, Vector2 direction, float distance)
        {
            return Physics2D.Raycast(origin, direction, distance, layerMaskConfig.value);
        }

        #endregion

        #region 3D
        public bool RayCast3D(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance)
        {
            return Physics.Raycast(origin, direction, out hit, distance, layerMaskConfig.value);
        }

        public bool RayLineCast3D(Vector3 start, Vector3 end, out RaycastHit hit)
        {
            return Physics.Linecast(start, end, out hit, layerMaskConfig);
        }

        #endregion
    }
}