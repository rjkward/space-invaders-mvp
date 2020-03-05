using SpaceInvadersMVP.PhysicsObject;
using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Weapon {
    public class ProjectileWeapon : MonoBehaviour
    {
        [Inject]
        private Projectile.Factory _projectileFactory;

        public void Fire()
        {
            _projectileFactory.Create(transform, Config.ProjectileSpeed);
        }
    }
}
