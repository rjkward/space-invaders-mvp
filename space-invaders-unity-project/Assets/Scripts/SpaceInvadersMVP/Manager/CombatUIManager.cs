using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Util.Enum;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class CombatUIManager : UIManager<CombatState>, IInitializable
    {
        [Inject]
        private CombatSessionModel _sessionModel;

        public void Initialize()
        {
            _sessionModel.State.Subscribe(HandleEnterCombatState);
        }

        public override void Dispose()
        {
            _sessionModel.State.Unsubscribe(HandleEnterCombatState);
            base.Dispose();
        }

        private void HandleEnterCombatState(CombatState newState)
        {
            DisplayViewsForState(newState);
        }
    }
}
