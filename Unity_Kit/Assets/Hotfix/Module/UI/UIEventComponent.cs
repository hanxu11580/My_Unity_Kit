using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix
{
	/// <summary>
	/// 管理所有UI GameObject
	/// </summary>
	public class UIEventComponent: Entity
	{
		public static UIEventComponent Instance;

		public AUIEvent UIEvent_Default;

		public Dictionary<string, AUIEvent> UIEvents = new Dictionary<string, AUIEvent>();
		
		public Dictionary<int, Transform> UILayers = new Dictionary<int, Transform>();
	}
}