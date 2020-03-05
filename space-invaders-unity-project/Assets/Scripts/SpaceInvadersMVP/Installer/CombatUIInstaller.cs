using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer
{
    [CreateAssetMenu(fileName = "CombatUIInstaller", menuName = "Installers/CombatUIInstaller")]
    public class CombatUIInstaller : ScriptableObjectInstaller<CombatUIInstaller>
    {
        [SerializeField]
        private CombatState[] _activeStates;

        [SerializeField]
        private GameObject[] _prefabs;

        public override void InstallBindings()
        {
            Dictionary<CombatState, GameObject[]> map = BuildMap();
            Container.Bind<Dictionary<CombatState, GameObject[]>>().FromInstance(map).AsSingle();
        }

        private Dictionary<CombatState, GameObject[]> BuildMap()
        {
            Dictionary<CombatState, GameObject[]> map = new Dictionary<CombatState, GameObject[]>();
            List<GameObject> list = new List<GameObject>();
            foreach (CombatState state in Enum.GetValues(typeof(CombatState)))
            {
                list.Clear();
                for (int i = 0; i < _activeStates.Length; i++)
                {
                    CombatState activeStates = _activeStates[i];
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
