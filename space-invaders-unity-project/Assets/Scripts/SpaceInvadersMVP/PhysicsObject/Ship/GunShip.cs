using SpaceInvadersMVP.Agent;
using SpaceInvadersMVP.Weapon;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.PhysicsObject.Ship
{
    public abstract class GunShip : ShipBase
    {
        [InjectOptional]
        protected IShipGunner Gunner;

        [SerializeField]
        private ProjectileWeapon[] _weapons;

        private void Update()
        {
            if (Gunner == null)
            {
                return;
            }

            if (Gunner.ShouldFireThisFrame())
            {
                // if (_weapons.Length < 1)
                // {
                //     Debug.LogWarning("No weapons assigned for GunShip.");
                // }

                foreach (ProjectileWeapon weapon in _weapons)
                {
                    weapon.Fire();
                }
            }
        }
    }
}
