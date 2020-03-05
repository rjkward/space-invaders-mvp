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
    [Flags]
    public enum MainMenuState
    {
        None = 0,
        MainMenu = 1 << 0,
        HighScores = 1 << 1,
        LoadingCombat = 1 << 2
    }

    [Serializable]
    [Flags]
    public enum CombatState
    {
        None = 0,
        PreparingForWave  = 1 << 0,
        DuringWave = 1 << 1,
        GameOver  = 1 << 2,
        AfterCombatScreen  = 1 << 3,
        Loading  = 1 << 4
    }

    public enum CombatOutcome
    {
        None,
        EnemyReachedBase,
        NoMoreLives
    }
}
