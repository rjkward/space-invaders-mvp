using SpaceInvadersMVP.Manager;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.UI;
using Zenject;

namespace SpaceInvadersMVP.Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainMenuManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<MainMenuUIManager>().AsSingle().NonLazy();

            Container.Bind<MainMenuModel>().AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<HighScoresViewModel>().AsSingle();

            Container.DeclareSignal<RequestNewMainMenuStateSignal>();
        }
    }
}
