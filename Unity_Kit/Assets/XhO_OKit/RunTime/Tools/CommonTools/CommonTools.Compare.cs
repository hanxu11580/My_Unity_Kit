using System;

namespace XhO_OKit
{
    /// <summary>
    /// 常用比较
    /// </summary>
    public partial class CommonTools
    {
        /// <summary>
        /// 获取数组最大值索引值
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetMaxIndex<T>(T[] arr) where T : IComparable<T>
        {
            var index = 0;
            var value = arr[0];
            for (var i = 1; i < arr.Length; ++i)
            {
                var _value = arr[i];
                if (_value.CompareTo(value) > 0)
                {
                    value = _value;
                    index = i;
                }
            }
            return index;
        }
        
        /// <summary>
        /// 获取数组最小值索引值
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetMinIndex<T>(T[] arr) where T : IComparable<T>
        {
            var index = 0;
            var value = arr[0];
            for (var i = 1; i < arr.Length; ++i)
            {
                var _value = arr[i];
                if (_value.CompareTo(value) < 0)
                {
                    value = _value;
                    index = i;
                }
            }
            return index;
        }
    }
}