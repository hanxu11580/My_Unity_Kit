using System;

namespace XhO_OKit
{
    /// <summary>
    /// 时间拓展类
    /// </summary>
    public static class TimeExtension
    {
        /// <summary>
        /// 将时间格式化成标准年/月/日 时:分:秒格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDefaultDateString(this DateTime time)
        {
            return time.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
