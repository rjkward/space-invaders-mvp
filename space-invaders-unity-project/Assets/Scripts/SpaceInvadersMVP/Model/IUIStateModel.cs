using SpaceInvadersMVP.DataBinding;

namespace SpaceInvadersMVP.Model
{
    public interface IUIStateModel<T>
    {
        public BindableProperty<T> UIState { get; }
    }
}