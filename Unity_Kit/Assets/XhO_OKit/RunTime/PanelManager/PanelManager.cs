using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XhO_OKit
{
    /// <summary>
    /// 面板管理
    /// </summary>
    public class PanelManager : Singleton<PanelManager>
    {
        public Dictionary<PanelType, IPanel> panels = new Dictionary<PanelType, IPanel>();
        private Transform UIRoot;
        private readonly string uiRootAssetName = "";
        public PanelManager()
        {
            var uIRootPrefab = AssetManager.Instance.LoadAsset<GameObject>(uiRootAssetName, ".prefab", false, true, AssetType.PanelAsset);

            UIRoot = GameObject.Instantiate(uIRootPrefab).transform;
        }


        public void OpenPanel(PanelType type)
        {
            if (PanelExist(type))
            {
                panels[type].OpenPanel();
            }
            else
            {
                IPanel panel = CreatePanel(type);
                panel.OpenPanel();
            }
        }

        public void ClosePanel(PanelType type)
        {
            if (PanelExist(type))
            {
                panels[type].ClosePanel();
            }
            else
            {
                IPanel panel = CreatePanel(type);
                panel.ClosePanel();
            }
        }

        public void RemovePanel(PanelType type)
        {
            if (PanelExist(type))
            {
                panels.Remove(type);
            }
        }

        /// <summary>
        /// 是否存在某个面板
        /// </summary>
        private bool PanelExist(PanelType type)
        {
            return panels.ContainsKey(type);
        }

        private IPanel CreatePanel(PanelType type)
        {
            string assetName = type.TypeToString();
            var prefab = AssetManager.Instance.LoadAsset<GameObject>(assetName, ".prefab", false, true, AssetType.PanelAsset);
            GameObject go = GameObject.Instantiate(prefab, UIRoot);
            IPanel panel = go.GetComponent<IPanel>();
            panel.InitPanel();
            panel.ClosePanel();
            panels.Add(type, panel);
            return panel;
        }
    }

}