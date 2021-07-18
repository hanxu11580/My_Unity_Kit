using ET;
using System;
using UnityEngine;

namespace Hotfix
{
	[ObjectSystem]
    public class UIEventComponentAwakeSystem : AwakeSystem<UIEventComponent>
	{
		public override void Awake(UIEventComponent self)
		{
			UIEventComponent.Instance = self;
			
			GameObject uiRoot = GameObject.Find("/Global/UI");
			ReferenceCollector referenceCollector = uiRoot.GetComponent<ReferenceCollector>();
			
			self.UILayers.Add((int)UILayer.Hidden, referenceCollector.Get<GameObject>(UILayer.Hidden.ToString()).transform);
			self.UILayers.Add((int)UILayer.Low, referenceCollector.Get<GameObject>(UILayer.Low.ToString()).transform);
			self.UILayers.Add((int)UILayer.Mid, referenceCollector.Get<GameObject>(UILayer.Mid.ToString()).transform);
			self.UILayers.Add((int)UILayer.High, referenceCollector.Get<GameObject>(UILayer.High.ToString()).transform);


			self.UIEvent_Default = Activator.CreateInstance(typeof(AUIEvent_Default)) as AUIEvent;
			//self.UIEvent_Default = new AUIEvent_Default();
			//var uiEvents = Game.EventSystem.GetTypes(typeof (UIEventAttribute));
			//foreach (Type type in uiEvents)
			//{
			//	object[] attrs = type.GetCustomAttributes(typeof(UIEventAttribute), false);
			//	if (attrs.Length == 0)
			//	{
			//		continue;
			//	}

			//	UIEventAttribute uiEventAttribute = attrs[0] as UIEventAttribute;
			//	AUIEvent aUIEvent = Activator.CreateInstance(type) as AUIEvent;
			//	self.UIEvents.Add(uiEventAttribute.UIType, aUIEvent);
			//}
		}
	}
	
	/// <summary>
	/// 管理所有UI GameObject 以及UI事件
	/// </summary>
	public static class UIEventComponentSystem
	{
		public static async ETTask<UI> OnCreate(this UIEventComponent self, UIComponent uiComponent, string uiType)
		{
			try
			{
				// 一块创建
				// UI ui = await self.UIEvents[uiType].OnCreate(uiComponent);

				UI ui = await self.UIEvent_Default.OnCreate(uiComponent, uiType);
				UILayer uiLayer = ui.GameObject.GetComponent<UILayerScript>().UILayer;
				ui.GameObject.transform.SetParent(self.UILayers[(int)uiLayer], false);
				return ui;
			}
			catch (Exception e)
			{
				throw new Exception($"on create ui error: {uiType}", e);
			}
		}
		
		public static void OnRemove(this UIEventComponent self, UIComponent uiComponent, string uiType)
		{
			try
			{
				self.UIEvents[uiType].OnRemove(uiComponent);
			}
			catch (Exception e)
			{
				throw new Exception($"on remove ui error: {uiType}", e);
			}
			
		}
	}
}