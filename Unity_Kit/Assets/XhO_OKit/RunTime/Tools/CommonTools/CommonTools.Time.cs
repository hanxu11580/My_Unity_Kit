using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{

    public static partial class CommonTools
    {
        #region 计算时间差

        /// <summary>
        /// 获取间隔秒数
        /// </summary>
        /// <param name="startTimer"></param>
        /// <param name="endTimer"></param>
        /// <param name="onlySecond">仅计算秒数差忽略时分等</param>
        /// <returns></returns>
        public static int GetSubSeconds(DateTime startTimer, DateTime endTimer, bool onlySecond = false)
        {
            TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

            TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

            TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

            return onlySecond ? subTimer.Seconds : (int)subTimer.TotalSeconds;
        }

        /// <summary>
        /// 获取两个时间的相差多少分钟
        /// </summary>
        /// <param name="startTimer"></param>
        /// <param name="endTimer"></param>
        /// <param name="onlyMinute">仅计算分钟差</param>
        /// <returns></returns>
        public static int GetSubMinutes(DateTime startTimer, DateTime endTimer, bool onlyMinute = false)
        {
            TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

            TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

            TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

            return onlyMinute ? subTimer.Minutes : (int)subTimer.TotalMinutes;
        }


        /// <summary>
        /// 获取两个时间的相差多少小时
        /// </summary>
        /// <param name="startTimer"></param>
        /// <param name="endTimer"></param>
        /// <param name="onlyHour">仅计算小时差</param>
        /// <returns></returns>
        public static int GetSubHours(DateTime startTimer, DateTime endTimer, bool onlyHour = false)
        {
            TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

            TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

            TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

            return onlyHour ? subTimer.Hours : (int)subTimer.TotalHours;
        }

        /// <summary>
        /// 获取两个时间的相差多少天
        /// </summary>
        /// <param name="startTimer"></param>
        /// <param name="endTimer"></param>
        /// <param name="onlyDay">仅计算天数差</param>
        /// <returns></returns>
        public static int GetSubDays(DateTime startTimer, DateTime endTimer, bool onlyDay = false)
        {
            TimeSpan startSpan = new TimeSpan(startTimer.Ticks);

            TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);

            TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

            return onlyDay ? subTimer.Days : (int)subTimer.TotalDays;
        }

        #endregion
    }
}