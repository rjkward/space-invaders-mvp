using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI {
    public class WaveStartView : View<CombatHUDViewModel>
    {
        [SerializeField]
        private Text _text;

        [SerializeField]
        private Text _flavourText;

        private static readonly string[] FlavourOptions =
        {
            "you've got this, commander",
            "we're counting on you, pilot",
            "let's teach these alien scum a lesson",
            "you're the only thing that stands between them and earth",
            "let's finish the job",
            "maybe... we should try and talk to the aliens?",
            "wait... are we the baddies?",
        };

        private void OnEnable()
        {
            ViewModel.WaveDisplayString.Subscribe(HandleNewDisplayString);
        }

        private void OnDisable()
        {
            ViewModel.WaveDisplayString.Unsubscribe(HandleNewDisplayString);
        }

        private void HandleNewDisplayString(string s)
        {
            _text.text = s;
            float r = Random.value;
            r = r == 1f ? 0f : r;
            _flavourText.text = FlavourOptions[Mathf.FloorToInt(r * r * FlavourOptions.Length)];
        }
    }
}
