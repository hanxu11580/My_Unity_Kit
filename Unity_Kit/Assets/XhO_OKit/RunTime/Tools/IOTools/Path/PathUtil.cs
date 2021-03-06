using UnityEngine;

namespace XhO_OKit
{
    public static class PathUtil
    {
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="type"></param>
        /// <param name="folders"></param>
        /// <returns></returns>
        public static string GetPath(PathType type, params string[] folders)
        {
            string path = string.Empty;
            string subPath = string.Empty;
            switch (type)
            {
                case PathType.DataPath:
                    path = Application.dataPath;
                    break;
                case PathType.StreamingAssetsPath:
                    path = Application.streamingAssetsPath;
                    break;
                case PathType.PersistentDataPath:
                    path = Application.persistentDataPath;
                    break;
            }
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (type == PathType.PersistentDataPath && folders.Length == 1 && folders[0].Contains("/"))
                {//类似folders[0] = "NewFolder/"
                    return GetPath(path, folders[0]);
                }
            }
            else
            {//不是Andriod/IOS
                if (folders.Length == 1 && folders[0].Contains("/"))
                {
                    return GetPath(path, folders[0]);
                }
            }

            //folders.Length > 1
            for (int i = 0; i < folders.Length; i++)
            {
                if (i == folders.Length - 1)
                {//最后一个
                    subPath = $"{subPath}{folders[i]}";
                }
                else
                {
                    subPath = $"{subPath}{folders[i]}/";
                }
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    if (type == PathType.PersistentDataPath && !string.IsNullOrEmpty(subPath))
                    {
                        DirectoryUtil.CreateDirectory($"{path}/{subPath}");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(subPath))
                    {
                        DirectoryUtil.CreateDirectory($"{path}/{subPath}");
                    }
                }
            }
            return $"{path}/{subPath}";
        }
        public static string GetPlatformForAssetBundle()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "OSX";
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "IOS";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 两个路径拼接
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string GetPath(string path, string folder)
        {
            string[] folders = folder.Split('/');
            folder = folders[0];
            DirectoryUtil.CreateDirectory($"{path}/{folder}");
            string subPath = string.Empty;
            for (int i = 1; i < folders.Length; i++)
            {
                if (string.IsNullOrEmpty(folders[i])) continue; // 双斜杠//情况
                subPath = $"{subPath}{folders[i]}";//拼接个路径(下面已经加斜杠了,这里直接拼接)
                DirectoryUtil.CreateDirectory($"{path}/{folder}/{subPath}");
                if (i == folders.Length - 1) continue;//最后一个
                subPath = $"{subPath}/"; //加个斜杠
            }
            return $"{path}/{folder}/{subPath}";
        }
    }
}