using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class MainMenuManager : IInitializable, IDisposable
    {
        [Inject]
        private MainMenuModel _menuModel;

        [Inject]
        private SignalBus _signalBus;

        private static readonly Dictionary<MainMenuState, HashSet<MainMenuState>> AllowedTransitions =
            new Dictionary<MainMenuState, HashSet<MainMenuState>>
            {
                {
                    MainMenuState.None,
                    new HashSet<MainMenuState> {MainMenuState.MainMenu}
                },
                {
                    MainMenuState.MainMenu,
                    new HashSet<MainMenuState> {MainMenuState.HighScores, MainMenuState.LoadingCombat}
                },
                {
                    MainMenuState.HighScores,
                    new HashSet<MainMenuState> {MainMenuState.MainMenu}
                }
            };

        public void Initialize()
        {
            _signalBus.Subscribe<RequestNewMainMenuStateSignal>(HandleNewMainMenuStateRequest);
            ChangeState(MainMenuState.MainMenu);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RequestNewMainMenuStateSignal>(HandleNewMainMenuStateRequest);
        }

        private void HandleNewMainMenuStateRequest(RequestNewMainMenuStateSignal signal)
        {
            ChangeState(signal.State);
        }

        private void ChangeState(MainMenuState newState)
        {
            MainMenuState oldState = _menuModel.State.Value;
            if (!AllowedTransitions.TryGetValue(oldState, out HashSet<MainMenuState> possibleStates) ||
                !possibleStates.Contains(newState))
            {
                Debug.Log(
                    $"Attempted forbidden transition: {oldState.ToString()} to {newState.ToString()}");
                return;
            }

            _menuModel.State.Value = newState;
            if (newState == MainMenuState.LoadingCombat)
            {
                StartCombat();
            }
        }

        private void StartCombat()
        {
            _signalBus.Fire(new RequestNewGameStateSignal {State = GameState.Combat});
        }
    }
}
