using SpaceInvadersMVP.Agent;
using SpaceInvadersMVP.PhysicsObject.Ship;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "PlayerShipInstaller", menuName = "Installers/PlayerShipInstaller")]
    public class PlayerShipInstaller : ScriptableObjectInstaller<PlayerShipInstaller>
    {
        [SerializeField]
        private PlayerShip _playerShipPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerShip>().FromComponentInNewPrefab(_playerShipPrefab).AsSingle();
            Container.BindInterfacesTo<PlayerAgent>().AsSingle().WhenInjectedInto<PlayerShip>();
        }
    }
}
