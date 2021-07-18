using ET;
using System;
using UnityEngine;

namespace Hotfix
{

    public class HotfixMain
    {
        public static void Start()
        {
            ET.Game.Hotfix.HotfixUpdate = () => { Update(); };
            ET.Game.Hotfix.HotfixLateUpdate = () => { LateUpdate(); };
            ET.Game.Hotfix.HotfixApplicationQuit = () => { OnApplicationQuit(); };
            LitJsonHelper.Init();
            ProtobufHelper.Init();

            Game.EventSystem.Add(ET.Game.Hotfix.GetHotfixTypes());

            Game.EventSystem.Publish(new HotfixEventStruct.HotfixAppStart()).Coroutine();

            Debug.Log("Hotfix初始化完成");
        }

        private static void LateUpdate()
        {
            try
            {
                Game.EventSystem.LateUpdate();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Update()
        {
            try
            {
                Game.EventSystem.Update();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void OnApplicationQuit()
        {
            Game.Close();
        }
    }
}