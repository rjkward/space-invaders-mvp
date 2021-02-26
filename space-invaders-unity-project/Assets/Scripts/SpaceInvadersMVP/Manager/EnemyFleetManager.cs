using System;
using SpaceInvadersMVP.Agent;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.PhysicsObject.Ship;
using SpaceInvadersMVP.Signal;
using SpaceInvadersMVP.Spawn;
using SpaceInvadersMVP.Util;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Manager
{
    public class EnemyFleetManager : IInitializable, IDisposable
    {
        [Inject]
        private SpawnPointColumn[] _spawnPointColumns;

        [Inject]
        private PoolableGunShip.Factory _shipFactory;

        [Inject]
        private DiContainer _container;

        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private CombatSessionModel _sessionModel;

        private EnemyAgent[][] _fleetModel;

        private int _destroyedColumns;

        public void Initialize()
        {
            _sessionModel.State.Subscribe(HandleEnterCombatState);
            _signalBus.Subscribe<EnemyShipDestroyedSignal>(HandleEnemyShipDestroyed);
        }

        public void Dispose()
        {
            _sessionModel.State.Unsubscribe(HandleEnterCombatState);
            _signalBus.Unsubscribe<EnemyShipDestroyedSignal>(HandleEnemyShipDestroyed);
        }

        private void SpawnFleet()
        {
            _destroyedColumns = 0;
            if (_fleetModel == null)
            {
                _fleetModel = new EnemyAgent[_spawnPointColumns.Length][];
            }

            for (int columnIndex = 0; columnIndex < _spawnPointColumns.Length; columnIndex++)
            {
                Transform[] spawnPointColumn = _spawnPointColumns[columnIndex].Spawns;
                if (_fleetModel[columnIndex] == null)
                {
                    _fleetModel[columnIndex] = new EnemyAgent[spawnPointColumn.Length];
                }

                SpawnColumn(spawnPointColumn, columnIndex);
            }
        }

        private void SpawnColumn(Transform[] spawnPointColumn, int columnIndex)
        {
            for (int rowIndex = 0; rowIndex < spawnPointColumn.Length; rowIndex++)
            {
                Transform spawnPoint = spawnPointColumn[rowIndex];
                EnemyAgent agent = _fleetModel[columnIndex][rowIndex];
                if (agent == null)
                {
                    agent = _container.Instantiate<EnemyAgent>();
                    _fleetModel[columnIndex][rowIndex] = agent;
                    agent.SetFleetCoordinate(columnIndex, rowIndex);
                }

                agent.Reset();
                _shipFactory.Create(spawnPoint,
                                      agent,  // IShipPilot
                                      agent); // IShipGunner
                if (rowIndex == 0)
                {
                    agent.FireAtWill();
                }
            }
        }

        private void HandleEnterCombatState(CombatState newState)
        {
            if (newState == CombatState.DuringWave)
            {
                SpawnFleet();
            }
        }

        private void HandleEnemyShipDestroyed(EnemyShipDestroyedSignal signal)
        {
            EnemyAgent next = GetFirstAliveAgentInColumn(signal.FleetCoordinate.ColumnIndex);
            if (next != null)
            {
                next.FireAtWill();
                return;
            }

            _destroyedColumns++;
            if (_destroyedColumns == _fleetModel.Length)
            {
                _signalBus.Fire<FleetDestroyedSignal>();
            }
        }

        private EnemyAgent GetFirstAliveAgentInColumn(int columnIndex)
        {
            EnemyAgent[] column = _fleetModel[columnIndex];
            for (int i = 0; i < column.Length; i++)
            {
                EnemyAgent agent = column[i];
                if (!agent.ShipDestroyed)
                {
                    return agent;
                }
            }

            return null;
        }
    }
}
