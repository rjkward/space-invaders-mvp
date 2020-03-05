using SpaceInvadersMVP.FX;
using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    [CreateAssetMenu(fileName = "ExplosionInstaller", menuName = "Installers/ExplosionInstaller")]
    public class ExplosionInstaller : ScriptableObjectInstaller<ExplosionInstaller>
    {
        [SerializeField]
        private Explosion _explosionPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<Vector2, bool, Explosion, Explosion.Factory>()
            .FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(Config.InitialPooledExplosions)
                      .FromComponentInNewPrefab(_explosionPrefab)
                      .UnderTransformGroup("ExplosionPool"));
        }
    }
}
