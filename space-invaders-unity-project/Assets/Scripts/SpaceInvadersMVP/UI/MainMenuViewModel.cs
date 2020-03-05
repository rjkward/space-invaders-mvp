using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Util.Enum;

namespace SpaceInvadersMVP.UI
{
    public class MainMenuViewModel : ViewModel
    {
        public void HandleStartGameClicked()
        {
            SignalBus.Fire(new RequestNewMainMenuStateSignal
            {
                State = MainMenuState.LoadingCombat
            });
        }

        public void HandleHighScoresClicked()
        {
            SignalBus.Fire(new RequestNewMainMenuStateSignal
            {
                State = MainMenuState.HighScores
            });
        }
    }
}
