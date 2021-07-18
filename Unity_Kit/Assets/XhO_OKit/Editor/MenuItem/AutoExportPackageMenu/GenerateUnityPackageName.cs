
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace XhO_OKit
{
    public class GenerateUnityPackageName
    {
    #if UNITY_EDITOR
        //%e 代表command+e  %e
        [MenuItem("XhO_OKit/导出UnityPackage", priority = -1)]
        private static void OnClickMenuItem()
        {
            //复制到粘贴板
            //GUIUtility.systemCopyBuffer = "MFramework_" + DateTime.Now.ToString("yyyyMMdd_HH");
            //直接执行MenuItem中的东西
            //EditorApplication.ExecuteMenuItem("MFramework/导出UnityPackage");

            string assetPathName = "Assets/XhO_OKit";
            string fileName = "XhO_OKit" + DateTime.Now.ToString("yyyyMMdd_HH") + ".unitypackage";
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);//递归包含子目录
            Application.OpenURL("file://" + Application.dataPath);
            Debug.Log("导出完成，文件名为：XhO_OKit" + DateTime.Now.ToString("yyyyMMdd_HH"));
           
        }
    #endif
    }

}
