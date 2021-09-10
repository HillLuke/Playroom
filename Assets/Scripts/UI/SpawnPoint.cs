using Assets.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SpawnPoint : MonoBehaviour, IDrawGizmo
    {
        [ShowInInspector]
        public bool DrawGizmo { get; set; }

        public void OnDrawGizmos()
        {
            // Draw a yellow cube at the transform position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(gameObject.transform.position, 0.5f);
        }
    }
}