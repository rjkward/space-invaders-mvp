using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI
{
    public class HighScoresView : View<HighScoresViewModel>
    {
        [SerializeField]
        private ScoreEntryView[] _scoreEntries;

        [SerializeField]
        private Button _backButton;

        private void OnEnable()
        {
            for (int i = 0; i < ViewModel.HighScores.Count; i++)
            {
                if (i >= _scoreEntries.Length)
                {
                    break;
                }

                _scoreEntries[i].SetValues(ViewModel.HighScores[i]);
            }

            _backButton.onClick.AddListener(ViewModel.BackClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(ViewModel.BackClicked);
        }
    }
}
