using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer
{
	public abstract class UIInstaller<T> : ScriptableObjectInstaller<UIInstaller<T>> where T : Enum
	{
		[SerializeField]
		private List<ViewConfig> _viewConfigs;

		public override void InstallBindings()
		{
			Dictionary<T, List<GameObject>> stateToViewsMap = BuildMap();
			Container.Bind<Dictionary<T, List<GameObject>>>().FromInstance(stateToViewsMap).AsSingle();
		}

		private Dictionary<T, List<GameObject>> BuildMap()
		{
			Dictionary<T, List<GameObject>> stateToViewsMap = new Dictionary<T, List<GameObject>>();
			foreach (var viewConfig in _viewConfigs)
			{
				foreach (var state in viewConfig.ActiveStates)
				{
					if (!stateToViewsMap.TryGetValue(state, out List<GameObject> views))
					{
						views = new List<GameObject>();
						stateToViewsMap.Add(state, views);
					}
					
					views.Add(viewConfig.View);
				}
			}
			
			return stateToViewsMap;
		}

		[Serializable]
		private struct ViewConfig
		{
			public GameObject View;
			public List<T> ActiveStates;
		}
	}
}