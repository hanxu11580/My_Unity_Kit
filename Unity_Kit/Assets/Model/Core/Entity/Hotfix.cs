using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ET
{

    public class Hotfix
    {
#if ILRuntime
		private ILRuntime.Runtime.Enviorment.AppDomain appDomain;
		private MemoryStream hotfixDllStream;
		private MemoryStream hotfixPdbStream;
#else
        private Assembly hotfixAssembly;
#endif
        private IStaticMethod hotfixInit;
        private List<Type> hotfixTypes;

        public Action HotfixUpdate;
        public Action HotfixLateUpdate;
        public Action HotfixApplicationQuit;

        public void GotoHotfix()
        {
#if ILRuntime
			ILHelper.InitILRuntime(appDomain);
#endif
            hotfixInit.Run();
        }

        public List<Type> GetHotfixTypes()
        {
            return hotfixTypes;
        }

#if ILRuntime
        public ILRuntime.Runtime.Enviorment.AppDomain GetAppDomain()
        {
            return appDomain;
        }
#endif
        public void LoadHotfixAssembly()
        {
            Game.Scene.GetComponent<ResourcesComponent>().LoadBundle($"code.unity3d");

            byte[] hotfixAssBytes = ((TextAsset)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("code.unity3d", "Hotfix.dll")).bytes;
            byte[] hotfixPdbBytes = ((TextAsset)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("code.unity3d", "Hotfix.pdb")).bytes;
#if ILRuntime
            Log.Debug($"当前使用的是ILRuntime模式");
			appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

			hotfixDllStream = new MemoryStream(hotfixAssBytes);
			hotfixPdbStream = new MemoryStream(hotfixPdbBytes);

			appDomain.LoadAssembly(hotfixDllStream, hotfixPdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

			hotfixInit = new ILStaticMethod(appDomain, "Hotfix.HotfixMain", "Start", 0);
			
			hotfixTypes = appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();

#else
            Log.Debug($"当前使用的是Mono模式");

            hotfixAssembly = Assembly.Load(hotfixAssBytes, hotfixPdbBytes);

            Type hotfixInit = hotfixAssembly.GetType("Hotfix.HotfixMain");

            hotfixInit = new MonoStaticMethod(hotfixInit, "Start");

            hotfixTypes = hotfixAssembly.GetTypes().ToList();
#endif

            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"code.unity3d");
        }

    }
}