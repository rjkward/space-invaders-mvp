using UnityEngine;

namespace SpaceInvadersMVP.Agent {
    public interface IShipPilot
    {
        Vector2 GetDeltaPositionThisFixedFrame();

        void NotifyShipDestroyed();
    }
}
