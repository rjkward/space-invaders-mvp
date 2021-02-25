using SpaceInvadersMVP.DataBinding;
using SpaceInvadersMVP.Util.Enum;

namespace SpaceInvadersMVP.Model
{
    public class MainMenuModel : IUIStateModel<MainMenuState>
    {
        public MutableBindableProperty<MainMenuState> State = new MutableBindableProperty<MainMenuState>();
        public BindableProperty<MainMenuState> UIState => State;
    }
}
