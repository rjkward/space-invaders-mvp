using System;

namespace SpaceInvadersMVP.Util.Enum
{
    public enum GameState
    {
        None,
        MainMenu,
        Combat
    }

    [Serializable]
    public enum MainMenuState
    {
        None,
        MainMenu,
        HighScores,
        LoadingCombat
    }

    [Serializable]
    public enum CombatState
    {
        None,
        PreparingForWave,
        DuringWave,
        GameOver,
        AfterCombatScreen,
        Loading
    }

    public enum CombatOutcome
    {
        None,
        EnemyReachedBase,
        NoMoreLives
    }
}
