using SpaceInvadersMVP.Agent;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.PhysicsObject.Ship {
    public abstract class ShipBase : ExplodablePhysicsObject
    {
        [InjectOptional]
        protected IShipPilot Pilot;

        private void FixedUpdate()
        {
            if (Pilot == null)
            {
                return;
            }

            Vector2 newPosition = Rigidbody2D.position + Pilot.GetDeltaPositionThisFixedFrame();
            Rigidbody2D.MovePosition(newPosition);
        }

        protected override void Explode()
        {
            Pilot.NotifyShipDestroyed();
            gameObject.SetActive(false);
        }
    }
}
