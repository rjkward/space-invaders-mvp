using System;
using SpaceInvadersMVP.Agent;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.PhysicsObject.Ship
{
    public class PoolableGunShip : GunShip, IPoolable<Transform, IShipPilot, IShipGunner, IMemoryPool>,
    IDisposable
    {
        private IMemoryPool _pool;

        protected override void Explode()
        {
            Pilot.NotifyShipDestroyed();
            Dispose();
        }

        public void OnSpawned(Transform spawnPoint, IShipPilot pilot, IShipGunner gunner,
            IMemoryPool pool)
        {
            Pilot = pilot;
            Gunner = gunner;
            _pool = pool;
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }

        public void OnDespawned()
        {
            Gunner = null;
            Pilot = null;
            _pool = null;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Transform, IShipPilot, IShipGunner, PoolableGunShip> { }
    }
}
