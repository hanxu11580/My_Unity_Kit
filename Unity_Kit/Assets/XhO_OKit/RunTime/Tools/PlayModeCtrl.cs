#if UNITY_EDITOR
using UnityEditor;

namespace XhO_OKit
{
    /// <summary>
    /// 控制PlayMode状态
    /// </summary>
    public static class PlayModeCtrl
    {
        /// <summary>
        /// 恢复播放
        /// </summary>
        public static void Play()
        {
            EditorApplication.isPlaying = true;
        }
        
        /// <summary>
        /// 暂停
        /// </summary>
        public static void Pause()
        {
            EditorApplication.isPaused = true;
        }
        
        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            EditorApplication.isPlaying = false;
        }
    }   
}
#endif
