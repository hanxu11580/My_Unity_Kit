using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace XhO_OKit
{
    public class AssetManager : Singleton<AssetManager>
    {
        public Dictionary<string, AssetData> assetDict = new Dictionary<string, AssetData>();
        private string GetAssetPath(string name, params string[] types)
        {
            if (types.Length == 0) return name;
            string path = string.Empty;
            for (int i = 0; i < types.Length; i++)
            {
                path = $"{path}{types[i]}/";
                if (i == types.Length - 1)
                {
                    path = $"{path}{name}";
                }
            }
            return path;
        }
        private AssetData LoadAssetData<T>(string name, string suffix, bool isKeep, bool isAssetBundle, params string[] types) where T : Object
        {
            string path = GetAssetPath(name, types);
            if (assetDict.ContainsKey(path))
            {
                return assetDict[path];
            }
            else
            {
                AssetData assetData = new AssetData();
                Object asset = null;
#if AssetBundle
                if (isAssetBundle)
                {
#if !UNITY_EDITOR
                    asset = AssetBundleManager.Instance.LoadAsset($"Assets/Bundles/{path}{suffix}").LoadAsset<T>($"Assets/Bundles/{path}{suffix}");
#else
                    asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Bundles/{path}{suffix}");
#endif
                }
                else
                {
#if !UNITY_EDITOR
//开宏 isAssetBundle = false
                    asset = Resources.Load<T>(path);
#else
                    asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Resources/{path}{suffix}");
#endif
                }
#else //未定义AssetBundle
#if !UNITY_EDITOR
                asset = Resources.Load<T>(path);
#else
                asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Resources/{path}{suffix}");
#endif
#endif
                if (asset == null) return null;
                assetData.asset = asset;
                assetData.types = types;
                assetData.name = name;
                assetData.suffix = suffix;
                assetData.isKeep = isKeep;
                assetData.isAssetBundle = isAssetBundle;
                assetDict.Add(path, assetData);
                return assetData;
            }
        }
        private async Task<AssetData> LoadAssetDataAsync<T>(string name, string suffix, bool isKeep, bool isAssetBundle, params string[] types) where T : Object
        {
            await Task.Run(() => { });
            string path = GetAssetPath(name, types);
            if (assetDict.ContainsKey(path))
            {
                return assetDict[path];
            }
            else
            {
                AssetData assetData = new AssetData();
                Object asset = null;
#if AssetBundle
                if (isAssetBundle)
                {
#if !UNITY_EDITOR
                    AsyncOperationHandle<T> handler = Addressables.LoadAssetAsync<T>($"Assets/Bundles/{path}{suffix}");
                    await handler.Task;
                    asset = handler.Result;
#else
                    asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Bundles/{path}{suffix}");
#endif
                }
                else
                {
#if !UNITY_EDITOR
                    asset = Resources.Load<T>(path);
#else
                    asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Resources/{path}{suffix}");
#endif
                }
#else
#if !UNITY_EDITOR
                asset = Resources.Load<T>(path);
#else
                asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/Resources/{path}{suffix}");
#endif
#endif
                if (asset == null) return null;
                assetData.asset = asset;
                assetData.types = types;
                assetData.name = name;
                assetData.suffix = suffix;
                assetData.isKeep = isKeep;
                assetData.isAssetBundle = isAssetBundle;
                assetDict.Add(path, assetData);
                return assetData;
            }
        }

        private void UnloadAsset(AssetData assetData)
        {
            if (!assetData.isKeep)
            {
#if AssetBundle
                if (assetData.isAssetBundle)
                {
#if !UNITY_EDITOR
                    AssetBundleManager.Instance.UnloadAsset($"Assets/Bundles/{GetAssetPath(assetData.name, assetData.types)}/{assetData.suffix}");
                    //Addressables.Release(assetData.asset);
#endif
                }
                else
                {
                    if (assetData.asset.GetType() != typeof(GameObject) && assetData.asset.GetType() != typeof(Component))
                    {
                        //Resources.UnloadAsset仅能释放非GameObject和Component的资源 比如Texture Mesh等真正的资源 对于由Prefab加载出来的Object或Component,则不能通过该函数来进行释放
                        Resources.UnloadAsset(assetData.asset);
                    }
                }
#else
                if (assetData.asset.GetType() != typeof(GameObject) && assetData.asset.GetType() != typeof(Component))
                {
                    //Resources.UnloadAsset仅能释放非GameObject和Component的资源 比如Texture Mesh等真正的资源 对于由Prefab加载出来的Object或Component,则不能通过该函数来进行释放
                    Resources.UnloadAsset(assetData.asset);
                }
#endif
            }
        }
        public T LoadAsset<T>(string name, string suffix, bool isKeep, bool isAssetBundle, params string[] types) where T : Object
        {
            AssetData assetData = LoadAssetData<T>(name, suffix, isKeep, isAssetBundle, types);
            if (assetData == null) return null;
            return (T)assetData.asset;
        }
        public async Task<T> LoadAssetAsync<T>(string name, string suffix, bool isKeep, bool isAssetBundle, params string[] types) where T : Object
        {
            AssetData assetData = await LoadAssetDataAsync<T>(name, suffix, isKeep, isAssetBundle, types);
            if (assetData == null) return null;
            return (T)assetData.asset;
        }
        public GameObject InstantiateAsset(string name, bool isKeep, bool isAssetBundle, params string[] types)
        {
            AssetData assetData = LoadAssetData<GameObject>(name, ".prefab", isKeep, isAssetBundle, types);
            if (assetData == null) return null;
            GameObject gameObject = (GameObject)Object.Instantiate(assetData.asset);
            gameObject.name = name;
            return gameObject;
        }
        public async Task<GameObject> InstantiateAssetAsync(string name, bool isKeep, bool isAssetBundle, params string[] types)
        {
            AssetData assetData = await LoadAssetDataAsync<GameObject>(name, ".prefab", isKeep, isAssetBundle, types);
            if (assetData == null) return null;
            GameObject gameObject = (GameObject)Object.Instantiate(assetData.asset);
            gameObject.name = name;
            return gameObject;
        }

        public void UnloadAsset(string name, params string[] types)
        {
            string path = GetAssetPath(name, types);
            if (assetDict.ContainsKey(path))
            {
                AssetData assetData = assetDict[path];
                UnloadAsset(assetData);
                assetDict.Remove(path);
            }
        }
        public void UnloadAssets()
        {
            string assetNames = string.Empty;
            foreach (string item in assetDict.Keys)
            {
                if (!assetDict[item].isKeep)
                {
                    assetNames = $"{assetNames}{item},";
                }
            }
            foreach (string item in assetNames.Split(','))
            {
                if (string.IsNullOrEmpty(item)) continue;
                AssetData assetData = assetDict[item];
                UnloadAsset(assetData);
                assetDict.Remove(item);
            }
        }
        public void UnloadAllAssets()
        {
            foreach (AssetData item in assetDict.Values)
            {
                UnloadAsset(item);
            }
            Resources.UnloadUnusedAssets();
            assetDict.Clear();
            GC.Collect();
        }
    }
}