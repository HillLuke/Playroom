using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interactable.Listener
{
[RequireComponent(typeof(Collider))]
public class TriggerListener : MonoBehaviour
{
    public UnityEvent<GameObject> TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent.Invoke(other.gameObject);
    }
}
}