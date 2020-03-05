using System.Collections.ObjectModel;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util;
using SpaceInvadersMVP.Util.Enum;
using Zenject;

namespace SpaceInvadersMVP.UI {
    public class HighScoresViewModel : ViewModel
    {
        [Inject]
        private HighScoreModel _model;

        [Inject]
        private SignalBus _signalBus;

        public ReadOnlyCollection<HighScore> HighScores => _model.HighScoreList;

        public void BackClicked()
        {
            _signalBus.Fire(new RequestNewMainMenuStateSignal
            {
                State = MainMenuState.MainMenu
            });
        }
    }
}
