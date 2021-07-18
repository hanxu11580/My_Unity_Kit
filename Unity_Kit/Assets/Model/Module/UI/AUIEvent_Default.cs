using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    public class AUIEvent_Default : AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, string uiType)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(uiType.StringToAB());

            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(uiType.StringToAB(), uiType);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

            UI ui = EntityFactory.CreateWithParent<UI, string, GameObject>(uiComponent, uiType, gameObject);

            // �޷�֪���������ͣ��ⲿ�Լ���
            // ui.AddComponent<UILoginComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            
        }
    }
}