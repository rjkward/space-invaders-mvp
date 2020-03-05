using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "EventSystemInstaller", menuName = "Installers/EventSystemInstaller")]
    public class EventSystemInstaller : ScriptableObjectInstaller<EventSystemInstaller>
    {
        [SerializeField]
        private EventSystem _eventSystemPrefab;

        public override void InstallBindings()
        {
            Container.Bind<EventSystem>().FromComponentInNewPrefab(_eventSystemPrefab).AsSingle()
                     .NonLazy();
        }
    }
}
