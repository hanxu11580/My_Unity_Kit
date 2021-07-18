using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    public class GlobalProto
    {
        public string AssetBundleServerUrl;
        public string Address;

        public string GetUrl()
        {
            string url = this.AssetBundleServerUrl;
#if UNITY_ANDROID
			url += "Android/";
#elif UNITY_IOS
			url += "IOS/";
#elif UNITY_WEBGL
			url += "WebGL/";
#elif UNITY_STANDALONE_OSX
			url += "MacOS/";
#else
            url += "PC/";
#endif
            return url;
        }
    }


    public class BundleDownloaderComponentAwakeSystem : AwakeSystem<BundleDownloaderComponent>
    {
        public override void Awake(BundleDownloaderComponent self)
        {
            string globalStr = Resources.Load<TextAsset>("Config/GlobalProto").text;
            self.globalConfig = JsonHelper.FromJson<GlobalProto>(globalStr);
            self.bundles = new Queue<string>();
            self.downloadedBundles = new HashSet<string>();
            self.downloadingBundle = "";
        }
    }



    public class BundleDownloaderComponent : Entity
    {

        private VersionConfig remoteVersionConfig;

        public GlobalProto globalConfig;
        // 下载大小
        public long TotalSize;
        // 需要下载的文件名
        public Queue<string> bundles;

        public HashSet<string> downloadedBundles;

        public string downloadingBundle;

        public UnityWebRequestAsync webRequest;

        public int Progress
        {
            get
            {
                if (this.TotalSize == 0) return 0;

                long alreadyDownloadBytes = 0;
                foreach (string downloadedFileName in downloadedBundles)
                {
                    long size = this.remoteVersionConfig.FileInfoDict[downloadedFileName].Size;
                    alreadyDownloadBytes += size;
                }
                if(this.webRequest != null)
                { //正在下载
                    alreadyDownloadBytes += (long)this.webRequest.Request.downloadedBytes;
                }

                return (int)(alreadyDownloadBytes * 100f / this.TotalSize);
            }
        }

        public async ETTask StartAsync()
        {
            // 下载远程VersionConfig
            string remoteVersionUrl = "";
            try
            {
                using (UnityWebRequestAsync webRequestAsync = EntityFactory.CreateWithParent<UnityWebRequestAsync>(this))
                {
                    remoteVersionUrl = globalConfig.GetUrl() + "StreamingAssets/" + "Version.txt";

                    await webRequestAsync.DownloadAsync(remoteVersionUrl);

                    remoteVersionConfig = JsonHelper.FromJson<VersionConfig>(webRequestAsync.Request.downloadHandler.text);
                }
            }
            catch(Exception e)
            {
                throw new Exception($"url: {remoteVersionUrl}", e);
            }
            // 下载本地VersionConfig
            VersionConfig localVersionConfig = null;
            string versionPath = Path.Combine(PathHelper.AppResPath4Web, "Version.txt");
            using(UnityWebRequestAsync webRequestAsync = EntityFactory.CreateWithParent<UnityWebRequestAsync>(this))
            {
                await webRequestAsync.DownloadAsync(versionPath);
                localVersionConfig = JsonHelper.FromJson<VersionConfig>(webRequestAsync.Request.downloadHandler.text);
            }
            // 删除
            DirectoryInfo directoryInfo = new DirectoryInfo(PathHelper.AppHotfixResPath);
            if (directoryInfo.Exists)
            {
                FileInfo[] fileInfos = directoryInfo.GetFiles();
                foreach (FileInfo fileInfo in fileInfos)
                {
                    if (remoteVersionConfig.FileInfoDict.ContainsKey(fileInfo.Name)) continue;
                    if (fileInfo.Name == "Version.txt") continue;
                    fileInfo.Delete();
                }
            }
            else
            {
                directoryInfo.Create();
            }

            foreach (FileVersionInfo fileVersionInfo in remoteVersionConfig.FileInfoDict.Values)
            {
                string localFileMD5 = BundleHelper.GetBundleMD5(localVersionConfig, fileVersionInfo.File);
                if(fileVersionInfo.MD5 == localFileMD5)
                {
                    continue;
                }

                this.bundles.Enqueue(fileVersionInfo.File);
                this.TotalSize += fileVersionInfo.Size;
            }
        }

        public async ETTask DownloadAsync()
        {
            if (this.bundles.Count == 0 && string.IsNullOrEmpty(this.downloadingBundle)) return;

            try
            {
                while (true)
                {
                    if (bundles.Count == 0) break;

                    this.downloadingBundle = this.bundles.Dequeue();

                    while (true)
                    {
                        try
                        {
                            using (this.webRequest = EntityFactory.CreateWithParent<UnityWebRequestAsync>(this))
                            {
                                await webRequest.DownloadAsync(this.globalConfig.GetUrl() + "StreamingAssets/" + this.downloadingBundle);
                                byte[] data = this.webRequest.Request.downloadHandler.data;
                                string path = Path.Combine(PathHelper.AppHotfixResPath, this.downloadingBundle);
                                using (FileStream fs = new FileStream(path, FileMode.Create))
                                {
                                    fs.Write(data, 0, data.Length);
                                }
                            }

                        } catch (Exception e)
                        {
                            Log.Error($"download bundle error: {this.downloadingBundle}\n{e}");
                            continue;
                        }
                        break;
                    }
                    this.downloadedBundles.Add(this.downloadingBundle);
                    this.downloadingBundle = "";
                    this.webRequest = null;
                }
            }catch(Exception e)
            {
                Log.Error(e);
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.remoteVersionConfig = null;
            this.TotalSize = 0;
            this.bundles = null;
            this.downloadedBundles = null;
            this.downloadingBundle = null;
        }
    }
}