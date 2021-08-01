using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interactable.Listener
{
    [RequireComponent(typeof(Collider))]
    public class ColliderListener : MonoBehaviour
    {
        public UnityEvent CollisionEnterEvent;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnterEvent.Invoke();
        }
    }
}