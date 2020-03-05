using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "BackgroundInstaller", menuName = "Installers/BackgroundInstaller")]
    public class BackgroundInstaller : ScriptableObjectInstaller<BackgroundInstaller>
    {
        [SerializeField]
        private Background _backgroundPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Background>().FromComponentInNewPrefab(_backgroundPrefab).AsSingle()
                     .NonLazy();
        }
    }
}
