
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    private LineRenderer curLine;//当前实例化出来的线

    private int posIndex;//当前线的Positions数组下标

    //线的参数
    public Color startColor = Color.black;//线开始的颜色
    public Color endColor = Color.black;//线结束的颜色
    public float width = 0.1f;//线宽度
    public int vertices = 3;//顶点数

    Vector3 _beforePos = default;

    /// <summary>
    /// 初始化线
    /// </summary>
    private void InitLine()
    {
        posIndex = 0;

        curLine = GetComponent<LineRenderer>();
        _beforePos = transform.position;
        curLine.startColor = startColor;
        curLine.endColor = endColor;
        curLine.startWidth = width;
        curLine.endWidth = width;

        curLine.positionCount = vertices;
    }

    private void Start()
    {
        InitLine();
    }

    private void Update()
    {
        if (Vector2.Distance(_beforePos, transform.position) >= 0.1f)
        {
            AddPositions(transform.position);
        }

    }

    /// <summary>
    /// 将线段添加到当前线的Positions数组中
    /// </summary>
    private void AddPositions(Vector3 pos)
    {
        if (posIndex >= vertices)
        {
            posIndex = 1;
        }
        else
        {
            posIndex++;
        }


        curLine.SetPosition(posIndex - 1, pos);
        _beforePos = pos;

    }
}
