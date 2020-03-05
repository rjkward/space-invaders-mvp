using System;
using SpaceInvadersMVP.DataBinding;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Util.Enum;
using Zenject;

namespace SpaceInvadersMVP.UI
{
    public class CombatHUDViewModel : ViewModel
    {
        [Inject]
        private CombatSessionModel _sessionModel;

        public BindableProperty<string> WaveDisplayString => _waveDisplayString;

        private MutableBindableProperty<string> _waveDisplayString = new MutableBindableProperty<string>();

        public BindableProperty<string> ScoreDisplayString => _scoreDisplayString;

        private MutableBindableProperty<string> _scoreDisplayString = new MutableBindableProperty<string>();

        public BindableProperty<string> LivesDisplayString => _livesDisplayString;

        private MutableBindableProperty<string> _livesDisplayString = new MutableBindableProperty<string>();

        public BindableProperty<string> OutcomeDisplayString => _outcomeDisplayString;

        private MutableBindableProperty<string> _outcomeDisplayString =
            new MutableBindableProperty<string>();

        public override void Initialize()
        {
            _sessionModel.WaveNumber.Subscribe(HandleWaveNumberChanged);
            _sessionModel.Score.Subscribe(HandleScoreChanged);
            _sessionModel.PlayerLives.Subscribe(HandleLivesChanged);
            _sessionModel.Outcome.Subscribe(HandleCombatOutcomeChanged);
        }

        public override void Dispose()
        {
            _sessionModel.WaveNumber.Unsubscribe(HandleWaveNumberChanged);
            _sessionModel.Score.Unsubscribe(HandleScoreChanged);
            _sessionModel.PlayerLives.Unsubscribe(HandleLivesChanged);
            _sessionModel.Outcome.Unsubscribe(HandleCombatOutcomeChanged);
        }

        private void HandleWaveNumberChanged(int waveCount)
        {
            _waveDisplayString.Value = $"wave {waveCount.ToString()}";
        }

        private void HandleScoreChanged(int score)
        {
            _scoreDisplayString.Value = $"score: {score.ToString()}";
        }

        private void HandleLivesChanged(int lives)
        {
            _livesDisplayString.Value = $"lives: {lives.ToString()}";
        }

        private void HandleCombatOutcomeChanged(CombatOutcome outcome)
        {
            switch (outcome)
            {
                case CombatOutcome.None:
                    _outcomeDisplayString.Value = string.Empty;
                    break;
                case CombatOutcome.EnemyReachedBase:
                    _outcomeDisplayString.Value = "the enemy reached your base";
                    break;
                case CombatOutcome.NoMoreLives:
                    _outcomeDisplayString.Value = "no more lives";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(outcome), outcome, null);
            }
        }
    }
}
