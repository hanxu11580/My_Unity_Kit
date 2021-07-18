using ET;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace ET
{

	public class Init : MonoBehaviour
	{
		private void Start()
		{
			try
			{
				SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

				DontDestroyOnLoad(gameObject);

				string[] assemblyNames = { "Unity.Model.dll" };

				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					string assemblyName = assembly.ManifestModule.Name;
					if (!assemblyNames.Contains(assemblyName))
					{
						continue;
					}
					Game.EventSystem.Add(assembly);
				}

				Game.Options = new Options();
				Game.EventSystem.Publish(new EventStruct.AppStart()).Coroutine();

			}
			catch (Exception e)
			{
				Log.Error(e);
			}

		}


		private void Update()
		{
			ThreadSynchronizationContext.Instance.Update();
			Game.Hotfix.HotfixUpdate?.Invoke();
			Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
			Game.Hotfix.HotfixLateUpdate?.Invoke();
			Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Hotfix.HotfixApplicationQuit?.Invoke();
			Game.Close();
		}
	}
}
