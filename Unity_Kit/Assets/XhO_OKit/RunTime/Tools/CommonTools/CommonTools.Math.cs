using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XhO_OKit
{
    public static partial class CommonTools
    {
        /// <summary> 是否为偶数 </summary>
        public static bool IsEvennumber(int val)
        {
            return (val & 1) == 0; //与1进行二进制 二进制偶数最后一位为 0
        }

        /// <summary>
        /// 返回两点之间距离（不开方）
        /// </summary>
        public static float GetVec3Distance(Vector3 v1, Vector3 v2)
        {
            float disX = v1.x - v2.x;
            float disY = v1.y - v2.y;
            float disZ = v1.z - v2.z;
            float dis = disX * disX + disY * disY + disZ * disZ;
            return dis;
        }


        static readonly string[] symbol = { "K", "M", "B", "T", "aa", "ab", "ac", "ad" };
        /// <summary>
        /// 数字加单位
        /// </summary>
        public static string NumUnit(double num)
        {
            int len = symbol.Length;
            double tempNum = num;
            if (tempNum < 10000)
            {
                return num.ToString("0");
            }

            int unitIndex = 0;
            while (tempNum / 10000 / 100 >= 1)
            {
                unitIndex++;
                if (unitIndex >= len)
                {
                    unitIndex = len - 1;
                    break;
                }
                tempNum /= 100;
            }
            return (tempNum / 1000).ToString("0.00") + symbol[unitIndex];
        }


    }

}
