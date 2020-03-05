using SpaceInvadersMVP.Signal;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Util
{
    public class EnemyDetector : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        private void OnTriggerEnter2D(Collider2D _)
        {
            _signalBus.Fire<EnemyDetectedSignal>();
        }
    }
}
