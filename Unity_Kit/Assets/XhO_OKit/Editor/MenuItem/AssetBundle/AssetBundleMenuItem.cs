using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace XhO_OKit
{
    public static class AssetBundleMenuItem
    {

        private readonly static string ABSPath = "Assets/XhO_OKit/Editor/ScriptableObject/AssetBundle/AssetBundleSetting.asset";

        [MenuItem("Assets/XhO_OKit/TagFileRule")]
        public static void TagFileRule()
        {
            if (File.Exists(ABSPath))
            {
                AssetBundleSetting assetBundleSetting = AssetDatabase.LoadAssetAtPath<AssetBundleSetting>(ABSPath);
                if (assetBundleSetting.assetBundleRuleList == null)
                {
                    assetBundleSetting.assetBundleRuleList = new List<AssetBundleRule>();
                }
                assetBundleSetting.assetBundleRuleList.Add(AssetBundleUtil.TagFileRule());
                assetBundleSetting.assetBundleDataList = AssetBundleUtil.BuildAssetBundleData(assetBundleSetting.assetBundleRuleList.ToArray());
            }
            else
            {
                AssetBundleSetting assetBundleSetting = ScriptableObject.CreateInstance<AssetBundleSetting>();
                assetBundleSetting.assetBundleRuleList = new List<AssetBundleRule>();
                assetBundleSetting.assetBundleRuleList.Add(AssetBundleUtil.TagFileRule());
                assetBundleSetting.assetBundleDataList = AssetBundleUtil.BuildAssetBundleData(assetBundleSetting.assetBundleRuleList.ToArray());
                AssetDatabase.CreateAsset(assetBundleSetting, ABSPath);
                AssetDatabase.Refresh();
            }
        }
        [MenuItem("Assets/XhO_OKit/TagDirectoryRule")]
        public static void TagDirectoryRule()
        {
            if (File.Exists(ABSPath))
            {
                AssetBundleSetting assetBundleSetting = AssetDatabase.LoadAssetAtPath<AssetBundleSetting>(ABSPath);
                if (assetBundleSetting.assetBundleRuleList == null)
                {
                    assetBundleSetting.assetBundleRuleList = new List<AssetBundleRule>();
                }
                assetBundleSetting.assetBundleRuleList.Add(AssetBundleUtil.TagDirectoryRule());
                assetBundleSetting.assetBundleDataList = AssetBundleUtil.BuildAssetBundleData(assetBundleSetting.assetBundleRuleList.ToArray());
            }
            else
            {
                AssetBundleSetting assetBundleSetting = ScriptableObject.CreateInstance<AssetBundleSetting>();
                assetBundleSetting.assetBundleRuleList = new List<AssetBundleRule>();
                assetBundleSetting.assetBundleRuleList.Add(AssetBundleUtil.TagDirectoryRule());
                assetBundleSetting.assetBundleDataList = AssetBundleUtil.BuildAssetBundleData(assetBundleSetting.assetBundleRuleList.ToArray());
                AssetDatabase.CreateAsset(assetBundleSetting, ABSPath);
                AssetDatabase.Refresh();
            }
        }
        [MenuItem("Assets/XhO_OKit/BuildAssetBundle")]
        public static void BuildAssetBundle()
        {
            if (File.Exists(ABSPath))
            {
                AssetBundleSetting assetBundleSetting = AssetDatabase.LoadAssetAtPath<AssetBundleSetting>(ABSPath);
                assetBundleSetting.buildId++;
                if (string.IsNullOrEmpty(assetBundleSetting.outputPath))
                {
                    assetBundleSetting.outputPath = "Assets/AssetBundles";
                }
                AssetBundleUtil.BuildAssetBundle(assetBundleSetting);
            }
        }

    }
}
