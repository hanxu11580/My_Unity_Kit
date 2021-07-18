using System;

namespace XhO_OKit
{
    /// <summary>
    /// ʱ����չ��
    /// </summary>
    public static class TimeExtension
    {
        /// <summary>
        /// ��ʱ���ʽ���ɱ�׼��/��/�� ʱ:��:���ʽ
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDefaultDateString(this DateTime time)
        {
            return time.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
