using SpaceInvadersMVP.Agent;
using SpaceInvadersMVP.Manager;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Spawn;
using SpaceInvadersMVP.UI;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Installer {
    public class CombatSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _playerSpawnPoint;

        [SerializeField]
        private EnemySpawnPoints _enemySpawnPoints;

        [SerializeField]
        private Camera _camera;

        public override void InstallBindings()
        {
            Container.Bind<CombatSessionModel>().AsSingle();
            InstallManagers();
            InstallSceneReferences();
            InstallAgents();
            InstallViewModels();
            DeclareCombatSignals();
        }

        private void InstallAgents()
        {
            Container.BindInterfacesAndSelfTo<EnemyPilotHivemind>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAggressionHivemind>().AsSingle();
        }

        private void InstallSceneReferences()
        {
            Container.Bind<Transform>().FromInstance(_playerSpawnPoint).AsSingle()
                     .WhenInjectedInto<PlayerShipManager>();

            Container.Bind<SpawnPointColumn[]>().FromInstance(_enemySpawnPoints.Columns).AsSingle();

            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        }

        private void InstallManagers()
        {
            Container.BindInterfacesTo<CombatManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerShipManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyFleetManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CombatUIManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreManager>().AsSingle().NonLazy();
        }

        private void InstallViewModels()
        {
            Container.BindInterfacesAndSelfTo<CombatHUDViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<AfterCombatViewModel>().AsSingle();
        }

        private void DeclareCombatSignals()
        {
            Container.DeclareSignal<EnemyShipDestroyedSignal>();
            Container.DeclareSignal<PlayerShipDestroyedSignal>();
            Container.DeclareSignal<FleetDestroyedSignal>();
            Container.DeclareSignal<PlayerLivesDepletedSignal>();
            Container.DeclareSignal<NameEnteredSignal>();
            Container.DeclareSignal<EnemyDetectedSignal>();
        }
    }
}
