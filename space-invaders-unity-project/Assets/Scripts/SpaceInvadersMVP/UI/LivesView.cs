using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI {
    public class LivesView : View<CombatHUDViewModel>
    {
        [SerializeField]
        private Text _text;

        private void Awake()
        {
            if (_text == null)
            {
                _text = GetComponent<Text>();
            }
        }

        private void OnEnable()
        {
            ViewModel.LivesDisplayString.Subscribe(HandleNewDisplayString);
        }

        private void OnDisable()
        {
            ViewModel.LivesDisplayString.Unsubscribe(HandleNewDisplayString);
        }

        private void HandleNewDisplayString(string s)
        {
            _text.text = s;
        }
    }
}
