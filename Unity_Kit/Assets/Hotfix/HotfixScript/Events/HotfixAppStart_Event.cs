using ET;
using UnityEngine;

namespace Hotfix
{
    [Event]
    public class HotfixAppStart_Event : AEvent<HotfixEventStruct.HotfixAppStart>
    {
        protected override async ETTask Run(HotfixEventStruct.HotfixAppStart a)
        {
            Game.Scene.AddComponent<UIEventComponent>();
            Game.Scene.AddComponent<UIComponent>();


            UI ui = await Game.Scene.GetComponent<UIComponent>().Create(UIType.UIStart);
            ui.AddComponent<StartUIComponent>();
        }
    }
}