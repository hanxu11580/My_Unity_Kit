using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace XhO_OKit
{
    public static class BytesExtension
    {
        public static string BytesToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] StringToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str.ToCharArray());
        }
    }
}