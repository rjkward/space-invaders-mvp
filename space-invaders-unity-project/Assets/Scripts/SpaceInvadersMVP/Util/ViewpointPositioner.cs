using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.Util
{
    public class ViewpointPositioner : MonoBehaviour
    {
        [Inject]
        private Camera _camera;

        [SerializeField]
        private Vector3 _viewpointPositionTarget;

        [SerializeField]
        private float XLimit;

        private void OnEnable()
        {
            Vector3 target = _camera.ViewportToWorldPoint(_viewpointPositionTarget);
            float sign = Mathf.Sign(target.x);
            target.x = sign * (Mathf.Max(XLimit, target.x * sign));
            transform.position = target;
        }
    }
}
