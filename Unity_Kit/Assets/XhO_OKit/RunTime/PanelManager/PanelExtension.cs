using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{

    public static class PanelExtension
    {
        public static string TypeToString(this PanelType type)
        {
            return $"{ type }Panel";
        }
    }
}