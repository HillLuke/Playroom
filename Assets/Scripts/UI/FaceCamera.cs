using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
        }
    }
}