using System;
using SpaceInvadersMVP.Model;
using SpaceInvadersMVP.Util.Enum;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Agent
{
    public class EnemyPilotHivemind : IInitializable, IDisposable
    {
        private Vector2 _delta;

        private float _lastDeltaCachedTime = -1;

        private float _beginMoveTime = 0f;

        private const float Speed = 1f;

        private bool _disabled;

        private HivemindMove CurrentMove => _moveCycle[_currentMoveIndex];

        private int _currentMoveIndex;

        private readonly HivemindMove[] _moveCycle = {
            new HivemindMove(Vector2.right, 3),
            new HivemindMove(Vector2.down, 1),
            new HivemindMove(Vector2.left, 3),
            new HivemindMove(Vector2.down, 1),
        };

        [Inject]
        private CombatSessionModel _combatSessionModel;

        public void Initialize()
        {
            _combatSessionModel.State.Subscribe(HandleEnterCombatState);
        }

        public void Dispose()
        {
            _combatSessionModel.State.Unsubscribe(HandleEnterCombatState);
        }

        public Vector2 GetDeltaPositionThisFixedFrame()
        {
            if (Time.fixedTime != _lastDeltaCachedTime)
            {
                CacheDeltaForThisFixedFrame();
                _lastDeltaCachedTime = Time.fixedTime;
            }

            return _disabled ? Vector2.zero : _delta;
        }

        private void CacheDeltaForThisFixedFrame()
        {
            if (Time.fixedTime - _beginMoveTime > CurrentMove.Time)
            {
                _currentMoveIndex = (_currentMoveIndex + 1) % _moveCycle.Length;
                _beginMoveTime = Time.fixedTime;
            }

            _delta = CurrentMove.Direction.normalized * (Speed * Time.fixedDeltaTime);
        }

        private struct HivemindMove
        {
            public readonly float Time;
            public readonly Vector2 Direction;
            public HivemindMove(Vector2 direction, float time)
            {
                Direction = direction;
                Time = time;
            }
        }

        private void HandleEnterCombatState(CombatState state)
        {
            if (state == CombatState.GameOver)
            {
                _disabled = true;
            }
        }
    }
}
