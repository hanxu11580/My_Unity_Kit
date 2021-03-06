using System;
using System.Linq;
using System.Text;

namespace XhO_OKit
{
    public static class StringExtension
    {
        public static byte[] GetBytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        

        #region 字符串首字符大小写

        /// <summary>
        /// 首字母小写写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToLower(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToLower() + input.Substring(1);
            return str;
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            string str = input.First().ToString().ToUpper() + input.Substring(1);
            return str;
        }
        
        #endregion
        
        
        #region 字符串直接采用StringBuilderp拼接
        
        /// <summary>
        /// 缓存字符串, ThreadStatic会使不同线程都会new一个新的_cachedStringBuilder
        /// 如果不这样设置，新线程会使用同一个
        /// 下面取自GameFramework 并添加了Concat拼接及注释
        /// </summary>
        [ThreadStatic]
        private static StringBuilder _cachedStringBuilder = null;
        
        public static StringBuilder GetStaticBuilder()
        {
            if (_cachedStringBuilder == null)
            {
                //最长1024
                _cachedStringBuilder = new StringBuilder(1024);
            }

            _cachedStringBuilder.Clear();
            return _cachedStringBuilder;
        }

        #region 格式化字符串
        
        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg0">字符串参数 0。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format(string format, object arg0)
        {
            if (format == null)
            {
                throw new XhO_OKitException("Format is invalid.");
            }

            GetStaticBuilder();
            // _cachedStringBuilder.Length = 0;
            _cachedStringBuilder.AppendFormat(format, arg0);
            return _cachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg0">字符串参数 0。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format(string format, object arg0, object arg1)
        {
            if (format == null)
            {
                throw new XhO_OKitException("Format is invalid.");
            }

            GetStaticBuilder();
            // _cachedStringBuilder.Length = 0;
            _cachedStringBuilder.AppendFormat(format, arg0, arg1);
            return _cachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <param name="format">字符串格式。</param>
        /// <param name="arg0">字符串参数 0。</param>
        /// <param name="arg1">字符串参数 1。</param>
        /// <param name="arg2">字符串参数 2。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            if (format == null)
            {
                throw new XhO_OKitException("Format is invalid.");
            }

            GetStaticBuilder();
            // _cachedStringBuilder.Length = 0;
            _cachedStringBuilder.AppendFormat(format, arg0, arg1, arg2);
            return _cachedStringBuilder.ToString();
        }

        /// <summary>
        /// 获取格式化字符串。
        /// </summary>
        /// <param name="format">字符串格式。</param>
        /// <param name="args">字符串参数。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string Format(string format, params object[] args)
        {
            if (format == null)
            {
                throw new XhO_OKitException("Format is invalid.");
            }

            if (args == null)
            {
                throw new XhO_OKitException("Args is invalid.");
            }

            GetStaticBuilder();
            // _cachedStringBuilder.Length = 0;
            _cachedStringBuilder.AppendFormat(format, args);
            return _cachedStringBuilder.ToString();
        }
        
        #endregion

        #region 拼接字符串

            public static string Concat(string s1, string s2)
            {
                GetStaticBuilder();
                _cachedStringBuilder.Append(s1);
                _cachedStringBuilder.Append(s2);
                return _cachedStringBuilder.ToString();
            }
            public static string Concat(string s1, string s2, string s3)
            {
                GetStaticBuilder();
                _cachedStringBuilder.Append(s1);
                _cachedStringBuilder.Append(s2);
                _cachedStringBuilder.Append(s3);
                return _cachedStringBuilder.ToString();
            }

        #endregion

        #endregion


        #region 字符串比较
        /// <summary>
        /// bytes比较字符串 如果判空请使用IsNullOrEmpty
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strOther"></param>
        /// <returns></returns>
        public static bool StringCompare(this string str, string strOther)
        {
            return str.Equals(strOther, StringComparison.Ordinal);
        }
        #endregion
    }
}
