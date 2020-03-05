using System;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class GameSceneManager : IInitializable, IDisposable
    {
        [Inject]
        private SignalBus _signalBus;

        public void Initialize()
        {
            _signalBus.Subscribe<RequestNewGameStateSignal>(HandleNewGameStateRequest);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RequestNewGameStateSignal>(HandleNewGameStateRequest);
        }

        private void HandleNewGameStateRequest(RequestNewGameStateSignal signal)
        {

            switch (signal.State)
            {
                case GameState.None:
                    Debug.LogError("This should never happen");
                    break;
                case GameState.MainMenu:
                    SceneManager.LoadSceneAsync(Config.MainMenuScene);
                    break;
                case GameState.Combat:
                    SceneManager.LoadSceneAsync(Config.CombatScene);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
