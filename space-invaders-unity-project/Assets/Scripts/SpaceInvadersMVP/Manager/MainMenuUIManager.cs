using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Util.Enum;
using Zenject;

namespace SpaceInvadersMVP.Manager {
    public class MainMenuUIManager : UIManager<MainMenuState>, IInitializable
    {
        [Inject]
        private MainMenuModel _menuModel;

        public void Initialize()
        {
            _menuModel.State.Subscribe(HandleEnterMainMenuState);
        }

        public override void Dispose()
        {
            _menuModel.State.Unsubscribe(HandleEnterMainMenuState);
            base.Dispose();
        }

        private void HandleEnterMainMenuState(MainMenuState newState)
        {
            DisplayViewsForState(newState);
        }
    }
}
