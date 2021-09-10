using Assets.Scripts.Player;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.Death();
        }
    }

    private void OnDrawGizmos()
    {
        var collider = gameObject.GetComponent<BoxCollider>();
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.transform.position, collider.size);
    }
}