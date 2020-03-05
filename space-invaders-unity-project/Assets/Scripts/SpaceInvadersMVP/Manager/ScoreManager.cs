using System;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class ScoreManager: IInitializable, IDisposable
    {
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private CombatSessionModel _sessionModel;

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyShipDestroyedSignal>(HandleEnemyShipDestroyed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyShipDestroyedSignal>(HandleEnemyShipDestroyed);
        }

        private void HandleEnemyShipDestroyed()
        {
            _sessionModel.Score.Value += Config.ScorePerShipPerWave * _sessionModel.WaveNumber.Value;
        }
    }
}
