namespace SpaceInvadersMVP.Util
{
    public static class Config
    {
        public const int StartingPlayerLives = 2;

        public const float ProjectileSpeed = 10f;
        public const float MinEnemyWeaponCooldown = 1f;
        public const float ExplosionDuration = 0.5f;

        public const int ScorePerShipPerWave = 5;
        public const int MaxHighScores = 3;

        public const int InitialPooledShips = 45;
        public const int InitialPooledProjectiles = 15;
        public const int InitialPooledExplosions = 10;

        public const string MainMenuScene = "Main";
        public const string CombatScene = "Combat";
    }
}
