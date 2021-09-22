using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interactable.Listener
{
    [RequireComponent(typeof(Collider))]
    public class TriggerListener : MonoBehaviour
    {
        public UnityEvent<GameObject> TriggerEnterEvent;
        public UnityEvent<GameObject> TriggerEnterExit;

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnterEvent.Invoke(other.gameObject);
        }


        private void OnTriggerExit(Collider other)
        {
            TriggerEnterExit.Invoke(other.gameObject);
        }
    }
}