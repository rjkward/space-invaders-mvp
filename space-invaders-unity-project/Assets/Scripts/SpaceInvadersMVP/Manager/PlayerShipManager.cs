using System;
using DG.Tweening;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.PhysicsObject.Ship;
using SpaceInvadersMVP.Signal;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class PlayerShipManager : IInitializable, IDisposable
    {
        [Inject]
        private PlayerShip _playerShip;

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private Transform _playerSpawnPoint;

        [Inject]
        private CombatSessionModel _sessionModel;

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerShipDestroyedSignal>(HandlePlayerShipDestroyed);
            SpawnPlayerShip();
        }


        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerShipDestroyedSignal>(HandlePlayerShipDestroyed);
        }

        private void SpawnPlayerShip()
        {
            _playerShip.transform.SetPositionAndRotation(_playerSpawnPoint.position,
                                                         _playerSpawnPoint.rotation);
            _playerShip.gameObject.SetActive(true);
        }

        private void HandlePlayerShipDestroyed()
        {
            if (_sessionModel.PlayerLives.Value < 1)
            {
                _signalBus.Fire<PlayerLivesDepletedSignal>();
                return;
            }

            _sessionModel.PlayerLives.Value--;
            DOVirtual.DelayedCall(1f, () => SpawnPlayerShip());
        }
    }
}
