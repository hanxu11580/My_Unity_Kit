using ET;
using System;
using System.Reflection;
using UnityEngine;

namespace ET
{

    public class AppStart_Event : AEvent<EventStruct.AppStart>
    {
        protected override async ETTask Run(EventStruct.AppStart a)
        {
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            Game.Scene.AddComponent<TimerComponent>();
            // 资源异步加载依赖协程锁
            Game.Scene.AddComponent<CoroutineLockComponent>();

            await BundleHelper.DownloadBundle();

            Game.Scene.AddComponent<ResourcesComponent>();

            Game.Hotfix.LoadHotfixAssembly();

            await TimerComponent.Instance.WaitAsync(1);

            Game.Scene.AddComponent<UIEventComponent>();
            Game.Scene.AddComponent<UIComponent>();

            Game.Hotfix.GotoHotfix();
        }
    }
}