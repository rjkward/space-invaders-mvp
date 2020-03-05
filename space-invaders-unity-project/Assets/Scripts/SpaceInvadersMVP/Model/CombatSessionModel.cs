using SpaceInvadersMVP.DataBinding;
using SpaceInvadersMVP.Util;
using SpaceInvadersMVP.Util.Enum;

namespace SpaceInvadersMVP.Model
{
    public class CombatSessionModel
    {
        public MutableBindableProperty<CombatState> State = new MutableBindableProperty<CombatState>(CombatState.None);
        public MutableBindableProperty<int> WaveNumber = new MutableBindableProperty<int>(0);
        public MutableBindableProperty<int> PlayerLives = new MutableBindableProperty<int>(Config.StartingPlayerLives);
        public MutableBindableProperty<int> Score = new MutableBindableProperty<int>(0);
        public MutableBindableProperty<CombatOutcome> Outcome =
            new MutableBindableProperty<CombatOutcome>(CombatOutcome.None);
    }
}
