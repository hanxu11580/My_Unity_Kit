#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// 动画预览
/// </summary>
[ExecuteInEditMode]
public class AnimatorPreview : MonoBehaviour
{
    private Animator animator = null;
    private float preTime = 0;//上一次运行的时间

    void Awake()
    {
        animator = GetComponent<Animator>();
        preTime = (float)EditorApplication.timeSinceStartup;
    }

    void OnEnable()
    {
        RegisterUpdate();
    }

    public void CustomUpdate()
    {
        //计算delta
        var delta = (float)EditorApplication.timeSinceStartup - preTime;
        preTime = (float)EditorApplication.timeSinceStartup;
        //更新
        animator.Update(delta);
    }

    void OnDisable()
    {
        UnResgisterUpdate();
    }

    private void RegisterUpdate()
    {
        EditorApplication.update += CustomUpdate;
    }

    private void UnResgisterUpdate()
    {
        EditorApplication.update -= CustomUpdate;
    }
}

#endif