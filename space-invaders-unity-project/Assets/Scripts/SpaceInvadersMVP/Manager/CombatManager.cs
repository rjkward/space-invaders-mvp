using System;
using System.Collections.Generic;
using DG.Tweening;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class CombatManager : IInitializable, IDisposable
    {
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private CombatSessionModel _sessionModel;

        private static readonly Dictionary<CombatState, HashSet<CombatState>> AllowedTransitions =
            new Dictionary<CombatState, HashSet<CombatState>>
            {
                {
                    CombatState.None,
                    new HashSet<CombatState> {CombatState.PreparingForWave, CombatState.GameOver}
                },
                {
                    CombatState.PreparingForWave,
                    new HashSet<CombatState> {CombatState.DuringWave, CombatState.GameOver}
                },
                {
                    CombatState.DuringWave,
                    new HashSet<CombatState> {CombatState.PreparingForWave, CombatState.GameOver}
                },
                {
                    CombatState.GameOver,
                    new HashSet<CombatState> {CombatState.AfterCombatScreen}
                },
                {
                    CombatState.AfterCombatScreen,
                    new HashSet<CombatState> {CombatState.Loading}
                },
            };

        public void Initialize()
        {
            _signalBus.Subscribe<FleetDestroyedSignal>(HandleFleetDestroyed);
            _signalBus.Subscribe<PlayerLivesDepletedSignal>(HandleLivesDepleted);
            _signalBus.Subscribe<EnemyDetectedSignal>(HandleEnemyDetected);
            _signalBus.Subscribe<NameEnteredSignal>(HandleNameEntered);
            _sessionModel.WaveNumber.Value = 0;
            NewWave();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<FleetDestroyedSignal>(HandleFleetDestroyed);
            _signalBus.Unsubscribe<PlayerLivesDepletedSignal>(HandleLivesDepleted);
            _signalBus.Unsubscribe<EnemyDetectedSignal>(HandleEnemyDetected);
            _signalBus.Unsubscribe<NameEnteredSignal>(HandleNameEntered);
        }

        private void HandleFleetDestroyed(FleetDestroyedSignal signal)
        {
            NewWave();
        }

        private void NewWave()
        {
            _sessionModel.WaveNumber.Value++;
            ChangeState(CombatState.PreparingForWave);
            DOVirtual.DelayedCall(3f, () => ChangeState(CombatState.DuringWave));
        }

        private void ChangeState(CombatState newState)
        {
            CombatState oldState = _sessionModel.State.Value;
            if (!AllowedTransitions.TryGetValue(
                    oldState, out HashSet<CombatState> possibleStates) ||
                !possibleStates.Contains(newState))
            {
                Debug.Log(
                    $"Attempted forbidden transition: {oldState.ToString()} to {newState.ToString()}");
                return;
            }

            _sessionModel.State.Value = newState;
        }

        private void HandleLivesDepleted()
        {
            EndGame(CombatOutcome.NoMoreLives);
        }

        private void HandleEnemyDetected()
        {
            EndGame(CombatOutcome.EnemyReachedBase);
        }

        private void EndGame(CombatOutcome outcome)
        {
            _sessionModel.Outcome.Value = outcome;
            ChangeState(CombatState.GameOver);
            DOVirtual.DelayedCall(2f, () => ChangeState(CombatState.AfterCombatScreen));
        }

        private void HandleNameEntered()
        {
            GoToMainMenu();
        }

        private void GoToMainMenu()
        {
            ChangeState(CombatState.Loading);
            _signalBus.Fire(new RequestNewGameStateSignal { State = GameState.MainMenu });
        }
    }
}
