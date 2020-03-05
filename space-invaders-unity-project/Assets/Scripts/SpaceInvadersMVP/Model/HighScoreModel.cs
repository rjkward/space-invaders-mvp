using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SpaceInvadersMVP.Util;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Model
{
    public class HighScoreModel : IInitializable
    {
        private const string HighScoreKey = "high_scores";

        public ReadOnlyCollection<HighScore> HighScoreList => _highScoreList.AsReadOnly();

        private List<HighScore> _highScoreList;

        public void Initialize()
        {
            Load();
        }

        public void AddHighscore(string playerName, int score)
        {
            _highScoreList.Add(new HighScore
            {
                Player = playerName,
                Score = score
            });

            _highScoreList.Sort((a, b) => b.Score - a.Score);
            while (_highScoreList.Count > Config.MaxHighScores)
            {
                _highScoreList.RemoveAt(_highScoreList.Count - 1);
            }

            Save();
        }


        private void Load()
        {
            string serialized = PlayerPrefs.GetString(HighScoreKey, string.Empty);
            if (!string.IsNullOrEmpty(serialized))
            {
                _highScoreList =
                    JsonUtility.FromJson<ScoreSerializationHelper>(serialized).Score.ToList();
            }
            else
            {
                _highScoreList = new List<HighScore>();
                AddHighscore("abi", 100);
                AddHighscore("dave", 500);
                AddHighscore("sharon", 1000);
            }
        }

        private void Save()
        {
            string serialized =
                JsonUtility.ToJson(new ScoreSerializationHelper {Score = _highScoreList.ToArray()});
            PlayerPrefs.SetString(HighScoreKey, serialized);
            PlayerPrefs.Save();
        }

        // Horrible hack to avoid having to install a proper serialization library and just using
        // Unity's built in serialization
        [Serializable]
        private class ScoreSerializationHelper
        {
            public HighScore[] Score;
        }
    }
}
