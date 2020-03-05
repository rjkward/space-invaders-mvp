using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI
{
    public class AfterCombatView : View<AfterCombatViewModel>
    {
        [SerializeField]
        private Text _scoreText;

        [SerializeField]
        private InputField _inputField;

        [SerializeField]
        private Button _confirmButton;

        private void OnEnable()
        {
            ViewModel.Score.Subscribe(HandleScoreChanged);
            ViewModel.ShowConfirmButton.Subscribe(ShowConfirmButton);
            _inputField.onValueChanged.AddListener(HandleEdit);
            _confirmButton.onClick.AddListener(Confirm);
        }

        private void OnDisable()
        {
            ViewModel.Score.Unsubscribe(HandleScoreChanged);
            ViewModel.ShowConfirmButton.Unsubscribe(ShowConfirmButton);
            _inputField.onValueChanged.RemoveListener(HandleEdit);
            _confirmButton.onClick.RemoveListener(Confirm);
        }

        private void HandleEdit(string s)
        {
            ViewModel.SetPlayerName(s);
        }

        private void HandleScoreChanged(int score)
        {
            _scoreText.text = score.ToString();
        }

        private void ShowConfirmButton(bool show)
        {
            _confirmButton.gameObject.SetActive(show);
        }

        private void Confirm()
        {
            ViewModel.Confirm();
        }
    }
}
