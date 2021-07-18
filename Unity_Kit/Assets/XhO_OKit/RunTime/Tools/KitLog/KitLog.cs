
using UnityEngine;
using System.Diagnostics;
namespace XhO_OKit
{
    /// <summary>
    /// 日志工具箱
    /// 清除 所有Debug 定义宏 DISABLE_DEBUG_LOGGING
    /// </summary>
    public static class KitLog
    {
        private static readonly string InfoPrefix = "<b><color=cyan>[Info]</color></b> ";
        private static readonly string WarningPrefix = "<b><color=yellow>[Warning]</color></b> ";
        private static readonly string ErrorPrefix = "<b><color=red>[Error]</color></b> ";

        /// <summary>
        /// 打印信息日志
        /// </summary>
        /// <param name="content">日志内容</param>
#if DISABLE_DEBUG_LOGGING
	    [Conditional("__NEVER_DEFINED__")]
#endif
        public static void Log(this string content)
        {
            UnityEngine.Debug.Log(InfoPrefix + content);
        }

#if DISABLE_DEBUG_LOGGING
	    [Conditional("__NEVER_DEFINED__")]
#endif
        public static void Log(System.Object content)
        {
            UnityEngine.Debug.Log(InfoPrefix + content);
        }


        /// <summary>
        /// 打印警告日志
        /// </summary>
        /// <param name="content">日志内容</param>
#if DISABLE_DEBUG_LOGGING
	    [Conditional("__NEVER_DEFINED__")]
#endif
        public static void Warning(this string content)
        {
            UnityEngine.Debug.LogWarning(WarningPrefix + content);
        }

        /// <summary>
        /// 打印错误日志
        /// </summary>
        /// <param name="content">日志内容</param>
#if DISABLE_DEBUG_LOGGING
	    [Conditional("__NEVER_DEFINED__")]
#endif
        public static void Error(this string content)
        {

            UnityEngine.Debug.LogError(ErrorPrefix + content);
        }
    }
}
