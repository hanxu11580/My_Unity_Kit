using UnityEngine;

namespace XhO_OKit
{
public static partial class CommonTools
{
    //曲线相关
    /// <param name="t">0到1的值，0获取曲线的起点，1获得曲线的终点</param>
    /// <param name="start">曲线的起始位置</param>
    /// <param name="center">决定曲线形状的控制点</param>
    /// <param name="end">曲线的终点</param>
    public static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }

    public static Vector3[] GetBezierPointArr(Transform startTrans, Transform endTrans, int resolution, float height)
    {
        var startPoint = startTrans.position;
        var endPoint = endTrans.position;
        var bezierControlPoint = (startPoint + endPoint) * 0.5f + (Vector3.up * height);

        Vector3[] path = new Vector3[resolution + 1];//resolution为int类型，表示要取得路径点数量，值越大，取得的路径点越多，曲线最后越平滑
        for (int i = 0; i <= resolution; i++)
        {
            var t = i / (float)resolution;//归化到0~1范围
            path[i] = GetBezierPoint(t, startPoint, bezierControlPoint, endPoint);//使用贝塞尔曲线的公式取得t时的路径点
        }

        return path;
    }
    public static Vector3[] GetBezierPointArr(Vector3 startPos, Vector3 endPos, int resolution, float height)
    {
        var startPoint = startPos;
        var endPoint = endPos;
        var bezierControlPoint = (startPoint + endPoint) * 0.5f + (Vector3.up * height);

        Vector3[] path = new Vector3[resolution + 1];//resolution为int类型，表示要取得路径点数量，值越大，取得的路径点越多，曲线最后越平滑
        for (int i = 0; i <= resolution; i++)
        {
            var t = i / (float)resolution;//归化到0~1范围
            path[i] = GetBezierPoint(t, startPoint, bezierControlPoint, endPoint);//使用贝塞尔曲线的公式取得t时的路径点
        }

        return path;
    }
}

}
