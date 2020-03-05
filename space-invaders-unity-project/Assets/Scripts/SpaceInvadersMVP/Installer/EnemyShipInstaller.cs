using SpaceInvadersMVP.Agent;
using SpaceInvadersMVP.PhysicsObject.Ship;
using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer
{
    [CreateAssetMenu(fileName = "EnemyShipInstaller", menuName = "Installers/EnemyShipInstaller")]
    public class EnemyShipInstaller : ScriptableObjectInstaller<EnemyShipInstaller>
    {
        [SerializeField]
        private PoolableGunShip _enemyShipPrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<Transform, IShipPilot, IShipGunner, PoolableGunShip,
                    PoolableGunShip.Factory>().FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(Config.InitialPooledShips)
                          .FromComponentInNewPrefab(_enemyShipPrefab)
                          .UnderTransformGroup("EnemyShipPool"));
        }
    }
}
