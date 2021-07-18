using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET;

namespace Hotfix
{
    [ObjectSystem]
    public class StartUIComponentAwake : AwakeSystem<StartUIComponent>
    {
        private Text text;

        public override void Awake(StartUIComponent self)
        {
            UI ui = self.Parent as UI;
            ReferenceCollector rc = ui.GameObject.GetComponent<ReferenceCollector>();
            Button btn = rc.Get<GameObject>("Btn_Enter").GetComponent<Button>();
            text = rc.Get<GameObject>("Text").GetComponent<Text>();
            btn.onClick.AddListener(OnClickBtn);
        }

        private void OnClickBtn()
        {
            text.text = "AAA";
        }
    }


    public class StartUIComponent : Entity
    {

    }
}