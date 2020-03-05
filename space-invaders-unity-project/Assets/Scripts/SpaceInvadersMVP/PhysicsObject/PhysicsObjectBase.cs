using UnityEngine;

namespace SpaceInvadersMVP.PhysicsObject {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class PhysicsObjectBase : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody2D;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}
