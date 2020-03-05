using SpaceInvadersMVP.DataBinding;
using SpaceInvadersMVP.Util.Enum;

namespace SpaceInvadersMVP.Model
{
    public class MainMenuModel
    {
        public MutableBindableProperty<MainMenuState> State = new MutableBindableProperty<MainMenuState>();
    }
}
