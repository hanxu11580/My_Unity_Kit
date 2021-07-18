using System.IO;

using UnityEditor;

namespace ET
{
    public static class BuildHelper
    {
        private const string relativeDirPrefix = "../Release";

        public static string BuildFolder = "../Release/{0}/StreamingAssets/";


        [MenuItem("Tools/web资源服务器")]
        public static void OpenFileServer()
        {
            ProcessHelper.Run("dotnet", "FileServer.dll", "../FileServer/");
        }

        public static void Build(PlatformType type, BuildAssetBundleOptions buildAssetBundleOptions, BuildOptions buildOptions, bool isBuildExe, bool isContainAB, bool clearFolder)
        {
            BuildTarget buildTarget = BuildTarget.StandaloneWindows;
            string exeName = "ET";
            switch (type)
            {
                case PlatformType.PC:
                    buildTarget = BuildTarget.StandaloneWindows64;
                    exeName += ".exe";
                    break;
                case PlatformType.Android:
                    buildTarget = BuildTarget.Android;
                    exeName += ".apk";
                    break;
                case PlatformType.IOS:
                    buildTarget = BuildTarget.iOS;
                    break;
                case PlatformType.MacOS:
                    buildTarget = BuildTarget.StandaloneOSX;
                    break;
            }

            string fold = string.Format(BuildFolder, type);

            if (clearFolder && Directory.Exists(fold))
            {
                Directory.Delete(fold, true);
                Directory.CreateDirectory(fold);
            }
            else
            {
                Directory.CreateDirectory(fold);
            }

            Log.Info("开始资源打包");
            BuildPipeline.BuildAssetBundles(fold, buildAssetBundleOptions, buildTarget);
            GenerateVersionInfo(fold);
            Log.Info("完成资源打包");

            if (isContainAB)
            {
                FileHelper.CleanDirectory("Assets/StreamingAssets/");
                FileHelper.CopyDirectory(fold, "Assets/StreamingAssets/");
            }

            if (isBuildExe)
            {
                AssetDatabase.Refresh();
                string[] levels = {
                    "Assets/Scenes/Init.unity",
                };
                Log.Info("开始EXE打包");
                BuildPipeline.BuildPlayer(levels, $"{relativeDirPrefix}/{exeName}", buildTarget, buildOptions);
                Log.Info("完成exe打包");
            }
        }

        private static void GenerateVersionInfo(string fold)
        {
            VersionConfig versionConfig = new VersionConfig();
            GenerateVersionProto(fold, versionConfig, "");
            
            using(FileStream fs = new FileStream($"{fold}/Version.txt", FileMode.Create))
            {
                byte[] bytes = JsonHelper.ToJson(versionConfig).ToByteArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private static void GenerateVersionProto(string fold, VersionConfig versionConfig, string relativePath)
        {
            foreach (string file in Directory.GetFiles(fold))
            {
                string md5 = MD5Helper.FileMD5(file);
                FileInfo fileInfo = new FileInfo(file);
                long fileSize = fileInfo.Length;
                string filePath = relativePath == "" ? fileInfo.Name : $"{relativePath}/{fileInfo.Name}";
                versionConfig.FileInfoDict.Add(filePath, new FileVersionInfo
                {
                    File = filePath,
                    MD5 = md5,
                    Size = fileSize
                });
            }

            foreach (string directory in Directory.GetDirectories(fold))
            {
                DirectoryInfo dinfo = new DirectoryInfo(directory);
                string rel = relativePath == "" ? dinfo.Name : $"{relativePath}/{dinfo.Name}";
                GenerateVersionProto($"{fold}/{dinfo.Name}", versionConfig, rel);
            }
        }
    }
}
