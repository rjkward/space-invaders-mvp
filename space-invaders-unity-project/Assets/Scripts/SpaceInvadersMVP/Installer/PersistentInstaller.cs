using SpaceInvadersMVP.Manager;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    public class PersistentInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInterfacesTo<GameSceneManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HighScoreModel>().AsSingle();
            Container.DeclareSignal<RequestNewGameStateSignal>();
        }
    }
}
