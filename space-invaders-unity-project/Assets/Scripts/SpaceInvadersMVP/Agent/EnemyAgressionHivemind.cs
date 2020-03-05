using System;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Util.Enum;
using Zenject;

namespace SpaceInvadersMVP.Agent
{
    public class EnemyAggressionHivemind: IInitializable, IDisposable
    {
        [Inject]
        private CombatSessionModel _combatSessionModel;

        public float Aggression { get; private set; }

        public void Initialize()
        {
            _combatSessionModel.WaveNumber.Subscribe(HandleNewWave);
            _combatSessionModel.State.Subscribe(HandleEnterCombatState);
        }

        public void Dispose()
        {
            _combatSessionModel.WaveNumber.Unsubscribe(HandleNewWave);
            _combatSessionModel.State.Unsubscribe(HandleEnterCombatState);
        }

        private void HandleEnterCombatState(CombatState newState)
        {
            if (newState == CombatState.GameOver)
            {
                Aggression = 0;
            }
        }

        private void HandleNewWave(int waveNumber)
        {
            Aggression = WaveCountToAggression(waveNumber);
        }

        private static float WaveCountToAggression(float waveCount)
        {
            return 1f - (1f / (1f + (0.2f * waveCount)));
        }
    }
}
