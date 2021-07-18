using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    public class MFieldOfView : MonoBehaviour
    {
        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;
        public LayerMask targetMask;
        public LayerMask obsMask;
        public List<Transform> visibleTargets = new List<Transform>();
        //Mesh相关
        public float meshResolution; //视野密度
        public MeshFilter viewMeshFilter;
        Mesh viewMesh;
        //如果2个顶点其中一个为是障碍物，且精度大于这个距离那么就需要进行精确计算一下
        //如果2个顶点 前后2个都是障碍物点，那么不用精确计算
        public float obsAccuracy;
        public int maxIteration; //最大迭代次数
        //是否找到目标
        public bool isFindTarget;


        //有时候找不到目标 可能是被地板的突起给挡住了
        private float normalizeY = 0.5f;


        void Start()
        {
            viewMeshFilter = GetComponent<MeshFilter>();
            viewMesh = new Mesh();
            viewMesh.name = "View Mesh";
            viewMeshFilter.mesh = viewMesh;
            //StartCoroutine(FindTargetsWithDelay(.1f));
        }
        /// <summary>
        /// 外部调用寻找目标
        /// </summary>
        public void StartFindTarget()
        {
            StopAllCoroutines();
            StartCoroutine(FindTargetsWithDelay(.1f));
        }

        private void LateUpdate()
        {
            //测试分区代码
            //int stepCount = Mathf.RoundToInt(viewAngle * meshResolution); //分成几个小扇形
            //float oneStepAngle = viewAngle / stepCount; //单个扇形角度
            //for (int i = 0; i < stepCount; i++)
            //{
            //    var newAngle = (-viewAngle * .5f) + oneStepAngle * i;
            //    Debug.DrawLine(transform.position, transform.position + DirFromAngle(newAngle) * viewRadius, Color.red);
            //}
            //真实画网格
            DrawFieldOfView();
        }

        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(delay);
                FindVisibleTargets();
            }
        }
        /// <summary>
        /// 角度转方向，这里已经加了自身的转向
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public Vector3 DirFromAngle(float angle)
        {
            //以x为起始轴
            //return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad));
            //以z为起始轴
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
        //--------private
        void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetInRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            for (int i = 0; i < targetInRadius.Length; i++)
            {
                Transform target = targetInRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                //这我修改了，正确为 Vector3.forward
                //我项目模型的z轴变了 所以为XhTools.DirFromAngle(transform.eulerAngles.y)
                //用的时候可能要改
                if (Vector3.Angle(CommonTools.DirFromAngle(transform.eulerAngles.y), dirToTarget) < viewAngle * .5f)
                {
                    //在范围内
                    //距离
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    Vector3 normalizePos = new Vector3(transform.position.x, normalizeY, transform.position.z);
                    if(!Physics.Raycast(normalizePos, dirToTarget,dstToTarget,obsMask))
                    {
                        //人和目标没有障碍物
                        visibleTargets.Add(target);
                    }
                }
            }
            //Debug.Log(visibleTargets.Count);
            if(visibleTargets.Count > 0)
                isFindTarget = true;
            else
                isFindTarget = false;
        }
        /// <summary>
        /// 对扇形区域裁剪
        /// </summary>
        /// <param name="globalAngle"></param>
        /// <returns></returns>
        ViewCastInfo ViewCast(float angle)
        {
            Vector3 dir = DirFromAngle(angle); //得到的是已经添加了自身旋转的
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obsMask))
                //射到了障碍就返回射到障碍的点
                return new ViewCastInfo(true, hit.point, hit.distance, angle);
            else
                //没有射到障碍返回射到圆半径位置
                return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, angle);
        }
        ///   <summary>
        ///  记录射线 射中的一些信息
        ///   </summary>
        public struct ViewCastInfo
        {
            // 射线是否射中目标
            public bool hit;
            // 射中的位置，（没有射中就是圆上的点呗）
            public Vector3 point;
            // 射中的距离，（没有射中就是半径呗）
            public float dst;
            // 就是 这个射线的角度
            public float angle;
            public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
            {
                hit = _hit;
                point = _point;
                dst = _dst;
                angle = _angle;
            }
        }
        /// <summary>
        /// 画出视野范围
        /// </summary>
        void DrawFieldOfView()
        {
            //发出射线获得顶点坐标
            int stepCount = Mathf.RoundToInt(viewAngle * meshResolution); //分成几个小扇形
            float oneStepAngle = viewAngle / stepCount; //单个扇形角度
            List<Vector3> castPoint = new List<Vector3>(); //用于存放射线检测后的点位置。
            ViewCastInfo previousCastPoint = new ViewCastInfo(); //上一次检测是点信息
            //为什么有 = 
            //包括两边
            for (int i = 0; i <= stepCount; i++)
            {
                //不用考虑自身旋转
                var newAngle = (-viewAngle * .5f) + oneStepAngle * i;
                ViewCastInfo newViewCastInfo = ViewCast(newAngle);
                if (i > 0)
                {
                    bool edgeDstThresholdExceeded = Mathf.Abs(previousCastPoint.dst - newViewCastInfo.dst) > obsAccuracy;
                    if (previousCastPoint.hit != newViewCastInfo.hit
                        || (previousCastPoint.hit && newViewCastInfo.hit && edgeDstThresholdExceeded))
                    {
                        EdgeInfo edge = FindEdge(previousCastPoint, newViewCastInfo);
                        if (edge.pointA != Vector3.zero)
                        {
                            castPoint.Add(edge.pointA);
                        }
                        if (edge.pointB != Vector3.zero)
                        {
                            castPoint.Add(edge.pointB);
                        }
                    }

                }
                castPoint.Add(newViewCastInfo.point);
                previousCastPoint = newViewCastInfo;
            }

            //利用顶点坐标绘制三角形Mesh
            int verticesCount = castPoint.Count + 1; //castPoint.Count会变的
            Vector3[] vertices = new Vector3[verticesCount]; //还有自身点
            int[] triangles = new int[(verticesCount - 2) * 3]; //几个三角面 * 3个顶点
            //规律
            // 012, 023, 034, 045...
            //  1    2    3    4 ...
            vertices[0] = Vector3.zero; //这个不是世界坐标 是 mesh所在Go为原点
            for (int i = 0; i < verticesCount-1; i++)
            {   
                vertices[i + 1] = transform.InverseTransformPoint(castPoint[i]);
                //例如分成4个面来绘制
                //0，1，2，3四个面就绘制完了
                if (i < verticesCount - 2)
                {
                    triangles[i * 3] = i * 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }
            viewMesh.Clear();
            viewMesh.vertices = vertices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }

        /// <summary>
        /// 二分法(这点判断逻辑没看懂)
        /// </summary>
        /// <param name="minViewCast"></param>
        /// <param name="maxViewCast"></param>
        /// <returns></returns>
        EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            float minAngle = minViewCast.angle;
            float maxAngle = maxViewCast.angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;

            for (int i = 0; i < maxIteration; i++)
            {
                float midAngle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(midAngle);

                bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > obsAccuracy;
                if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
                {
                    //
                    minAngle = midAngle;
                    minPoint = newViewCast.point;
                }
                else
                {
                    maxAngle = midAngle;
                    maxPoint = newViewCast.point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }

        /// <summary>
        /// 二分法数据结构
        /// </summary>
        public struct EdgeInfo
        {
            public Vector3 pointA;
            public Vector3 pointB;

            public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
            {
                pointA = _pointA;
                pointB = _pointB;
            }
        }
    }
}
