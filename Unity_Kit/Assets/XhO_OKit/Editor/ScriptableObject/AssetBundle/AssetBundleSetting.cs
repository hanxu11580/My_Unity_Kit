
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace XhO_OKit
{
    [CreateAssetMenu(fileName = "AssetBundleSetting", menuName = "CreateAssetBundleSetting", order = 1)]
    public class AssetBundleSetting : ScriptableObject
    {
        public int buildId;
        public string outputPath;
        public bool isCopyStreamingAssets;
        public List<AssetBundleRule> assetBundleRuleList;
        public List<AssetBundleData> assetBundleDataList;


        public void Clear()
        {
            buildId = 0;
            outputPath = string.Empty;
            isCopyStreamingAssets = true;
            assetBundleRuleList = null;
            assetBundleDataList = null;
        }
    }

    [CustomEditor(typeof(AssetBundleSetting))]
    public class AssetBundleSettingInspector : UnityEditor.Editor
    {
        AssetBundleSetting _setting;

        private void OnEnable()
        {
            _setting = target as AssetBundleSetting;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("清除配置"))
            {
                _setting.Clear();
            }
        }
    }
}