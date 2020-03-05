using SpaceInvadersMVP.FX;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.PhysicsObject {
    public abstract class ExplodablePhysicsObject : PhysicsObjectBase
    {
        [Inject]
        private Explosion.Factory _explosionPool;

        private void OnCollisionEnter2D(Collision2D _)
        {
            Explode();
            _explosionPool.Create(transform.position);
        }

        protected abstract void Explode();
    }
}
