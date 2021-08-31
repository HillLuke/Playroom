using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCameraConfig : MonoBehaviour
    {
        public GameObject Follow;
        public GameObject LookAt;

        private void Start()
        {
            if (Follow == null)
            {
                Debug.LogError("Follow not set");
            }

            if (LookAt == null)
            {
                Debug.LogError("LookAt not set");
            }
        }
    }
}