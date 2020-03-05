using SpaceInvadersMVP.Util;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceInvadersMVP.UI
{
    public class ScoreEntryView : MonoBehaviour
    {
        [SerializeField]
        private Text _nameText;

        [SerializeField]
        private Text _scoreText;

        public void SetValues(HighScore highScore)
        {
            _nameText.text = highScore.Player;
            _scoreText.text = highScore.Score.ToString();
        }
    }
}
