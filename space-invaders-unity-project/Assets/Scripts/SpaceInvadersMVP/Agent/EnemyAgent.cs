using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Agent
{
    public class EnemyAgent : IShipPilot, IShipGunner
    {
        public bool ShipDestroyed { get; private set; }

        [Inject]
        private EnemyPilotHivemind _navigationHivemind;

        [Inject]
        private EnemyAggressionHivemind _aggressionHivemind;

        [Inject]
        private SignalBus _signalBus;

        private FleetCoordinate _fleetCoordinate;

        private bool _canFire;

        private float _avgChancePerSecond;

        private float _lastFireTime;

        public void SetFleetCoordinate(int column, int row)
        {
	        _fleetCoordinate = new FleetCoordinate(column, row);
        }

        public void Reset()
        {
            _canFire = false;
            _lastFireTime = -Config.MinEnemyWeaponCooldown;
            ShipDestroyed = false;
        }

        public Vector2 GetDeltaPositionThisFixedFrame()
        {
            return _navigationHivemind.GetDeltaPositionThisFixedFrame();
        }

        public bool ShouldFireThisFrame()
        {
            if (!_canFire)
            {
                return false;
            }

            if (Time.time - _lastFireTime < Config.MinEnemyWeaponCooldown)
            {
                return false;
            }

            if (Random.value > _aggressionHivemind.Aggression * Time.deltaTime)
            {
                return false;
            }

            _lastFireTime = Time.time;
            return true;
        }

        public void NotifyShipDestroyed()
        {
            ShipDestroyed = true;
            _signalBus.Fire(new EnemyShipDestroyedSignal
            {
                FleetCoordinate = _fleetCoordinate
            });
        }

        public void FireAtWill()
        {
            _canFire = true;
        }
    }
}
