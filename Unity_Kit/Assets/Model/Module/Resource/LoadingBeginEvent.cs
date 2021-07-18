using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{

    public class LoadingFinishEvent : AEvent<EventStruct.LoadingBegin>
    {
        protected override async ETTask Run(EventStruct.LoadingBegin a)
        {
            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            while (true)
            {
                await timerComponent.WaitAsync(1);
                BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
                if(bundleDownloaderComponent == null)
                {
                    continue;
                }

                Log.Info($"下载进度: {bundleDownloaderComponent.Progress}%");
            }
        }
    }
}