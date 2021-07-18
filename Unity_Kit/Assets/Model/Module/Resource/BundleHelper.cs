using System;
using System.IO;

namespace ET
{
    public static class BundleHelper
	{
		public static async ETTask DownloadBundle()
		{
			if (Define.IsAsync)
			{
				try
				{
					using (BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.AddComponent<BundleDownloaderComponent>())
					{
						await bundleDownloaderComponent.StartAsync();
						Game.EventSystem.Publish(new EventStruct.LoadingBegin()).Coroutine();
						await bundleDownloaderComponent.DownloadAsync();
                    }
                }
				catch (Exception e)
				{
					//Log.Error(e);
					return;
				}
			}
		}

		public static string GetBundleMD5(VersionConfig localVersionConfig, string bundleName)
		{
			string path = Path.Combine(PathHelper.AppHotfixResPath, bundleName);
			if (File.Exists(path))
			{
				return MD5Helper.FileMD5(path);
			}
			
			if (localVersionConfig.FileInfoDict.ContainsKey(bundleName))
			{
				return localVersionConfig.FileInfoDict[bundleName].MD5;	
			}

			return "";
		}
	}
}
