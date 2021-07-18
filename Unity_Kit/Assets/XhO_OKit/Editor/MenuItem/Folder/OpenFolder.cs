
using UnityEditor;
using UnityEngine;


namespace XhO_OKit
{
    public static class OpenFolder
    {
        [MenuItem("XhO_OKit/文件夹/打开PersistentDataPath")]
        public static void OpenPersistentDataPath()
        {
            EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
        }
    }   
}
