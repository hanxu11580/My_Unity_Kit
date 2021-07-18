using System;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 协变学习 out
    /// </summary>
    public class CovarianceStudy : MonoBehaviour
    {
        private void Start()
        {
            string s1 = "123{0}";
            string s2 = "abc";
            Debug.Log(String.Concat(s1, 1));
            Debug.Log(String.Concat(s1, s2));
        }
    }
}


