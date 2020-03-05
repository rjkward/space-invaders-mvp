using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "MainMenuUIInstaller", menuName = "Installers/MainMenuUIInstaller")]
    public class MainMenuUIInstaller : ScriptableObjectInstaller<MainMenuUIInstaller>
    {
        [SerializeField]
        private MainMenuState[] _activeStates;

        [SerializeField]
        private GameObject[] _prefabs;

        public override void InstallBindings()
        {
            Dictionary<MainMenuState, GameObject[]> map = BuildMap();
            Container.Bind<Dictionary<MainMenuState, GameObject[]>>().FromInstance(map).AsSingle();
        }

        private Dictionary<MainMenuState, GameObject[]> BuildMap()
        {
            Dictionary<MainMenuState, GameObject[]> map = new Dictionary<MainMenuState, GameObject[]>();
            List<GameObject> list = new List<GameObject>();
            foreach (MainMenuState state in Enum.GetValues(typeof(MainMenuState)))
            {
                list.Clear();
                for (int i = 0; i < _activeStates.Length; i++)
                {
                    MainMenuState activeStates = _activeStates[i];
                    if ((activeStates & state) == state)
                    {
                        list.Add(_prefabs[i]);
                    }
                }

                if (list.Count < 1)
                {
                    continue;
                }

                map.Add(state, list.ToArray());
            }

            return map;
        }
    }
}
