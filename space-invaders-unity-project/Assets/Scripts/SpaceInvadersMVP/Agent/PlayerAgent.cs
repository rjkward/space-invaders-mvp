using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Agent {
    public class PlayerAgent : IShipPilot, IShipGunner
    {
        private const string Horizontal = "Horizontal";

        private const float MovementSpeed = 5f;

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private CombatSessionModel _sessionModel;

        public Vector2 GetDeltaPositionThisFixedFrame()
        {
            if (_sessionModel.State.Value == CombatState.GameOver)
            {
                return Vector2.zero;
            }

            return new Vector2(Input.GetAxis(Horizontal) * MovementSpeed * Time.fixedDeltaTime, 0f);
        }

        public void NotifyShipDestroyed()
        {
            _signalBus.Fire<PlayerShipDestroyedSignal>();
        }

        public bool ShouldFireThisFrame()
        {
            if (_sessionModel.State.Value != CombatState.DuringWave)
            {
                return false;
            }

            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}
