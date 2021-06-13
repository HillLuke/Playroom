using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interactable.Listener
{
[RequireComponent(typeof(Collider))]
public class TriggerListener : MonoBehaviour
{
    public UnityEvent TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent.Invoke();
    }
}
}