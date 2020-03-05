using SpaceInvadersMVP.DataBinding;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using Zenject;

namespace SpaceInvadersMVP.UI
{
    public class AfterCombatViewModel : ViewModel
    {
        [Inject]
        private HighScoreModel _highScoreModel;

        [Inject]
        private CombatSessionModel _sessionModel;

        public BindableProperty<int> Score => _sessionModel.Score;

        public BindableProperty<bool> ShowConfirmButton => _showConfirmButton;

        private MutableBindableProperty<bool> _showConfirmButton = new MutableBindableProperty<bool>();

        private string _name;

        public void SetPlayerName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _showConfirmButton.Value = false;
                return;
            }

            _name = name;
            _showConfirmButton.Value = true;
        }

        public void Confirm()
        {
            _highScoreModel.AddHighscore(_name, _sessionModel.Score.Value);
            SignalBus.Fire<NameEnteredSignal>();
        }
    }
}
