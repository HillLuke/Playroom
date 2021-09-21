using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets
{
    public class DeathCollider : MonoBehaviour, IDrawGizmo
    {
        [ShowInInspector]
        public bool DrawGizmo { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Death();
            }
        }

        public void OnDrawGizmos()
        {
            if (DrawGizmo)
            {
                var collider = gameObject.GetComponent<BoxCollider>();
                Gizmos.color = Color.red;
                Gizmos.DrawCube(gameObject.transform.position, collider.size);
            }
        }
    }
}