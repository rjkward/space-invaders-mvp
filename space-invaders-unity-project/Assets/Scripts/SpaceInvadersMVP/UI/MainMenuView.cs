using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI
{
    public class MainMenuView : View<MainMenuViewModel>
    {
        [SerializeField]
        private Button _startGameButton;

        [SerializeField]
        private Button _highScoreButton;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(ViewModel.HandleStartGameClicked);
            _highScoreButton.onClick.AddListener(ViewModel.HandleHighScoresClicked);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(ViewModel.HandleStartGameClicked);
            _highScoreButton.onClick.RemoveListener(ViewModel.HandleHighScoresClicked);
        }
    }
}
