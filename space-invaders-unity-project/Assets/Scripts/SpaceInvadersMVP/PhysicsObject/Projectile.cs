using System;
using SpaceInvadersMVP.PhysicsObject;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Weapon {
    public class Projectile : PhysicsObjectBase, IPoolable<Transform, float, IMemoryPool>,
                              IDisposable
    {
        private IMemoryPool _pool;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Dispose();
        }

        public void OnSpawned(Transform spawnPoint, float speed, IMemoryPool pool)
        {
            _pool = pool;
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D.velocity = transform.up * speed;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Transform, float, Projectile> { }
    }
}
