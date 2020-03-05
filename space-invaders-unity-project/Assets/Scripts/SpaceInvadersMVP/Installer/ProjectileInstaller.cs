using SpaceInvadersMVP.Util;
using SpaceInvadersMVP.Weapon;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "ProjectileInstaller", menuName = "Installers/ProjectileInstaller")]
    public class ProjectileInstaller : ScriptableObjectInstaller<ProjectileInstaller>
    {
        [SerializeField]
        private Projectile _projectilePrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<Transform, float, Projectile, Projectile.Factory>()
                     .FromMonoPoolableMemoryPool(
                         x => x.WithInitialSize(Config.InitialPooledProjectiles)
                               .FromComponentInNewPrefab(_projectilePrefab)
                               .UnderTransformGroup("ProjectilePool"));
        }
    }
}
