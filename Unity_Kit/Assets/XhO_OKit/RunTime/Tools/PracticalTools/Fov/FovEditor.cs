using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace XhO_OKit
{
#if UNITY_EDITOR
    [CustomEditor(typeof(MFieldOfView))]
    public class FovEditor : Editor
    {
        private void OnSceneGUI()
        {
            MFieldOfView fow = target as MFieldOfView;
            Handles.color = Color.white;

            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360f, fow.viewRadius);
            Vector3 viewStart = fow.DirFromAngle(-fow.viewAngle / 2);
            Vector3 viewEnd = fow.DirFromAngle(fow.viewAngle / 2);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewStart * fow.viewRadius);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewEnd * fow.viewRadius);

            Handles.color = Color.red;
            foreach (var target in fow.visibleTargets)
            {
                Handles.DrawLine(fow.transform.position, target.position);
            }
        }
    }
#endif
}
