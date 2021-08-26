using Assets.Scripts.Player.Movement;
using Assets.Scripts.Singletons;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraRig : MonoBehaviour
    {
        public Transform Follow;
        public Transform LookAt;
        public CinemachineFreeLook FreeLookMovement;

        private InputManager _inputManager;
        private bool _isPaused = false;

        private void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += PauseCameraInput;
            }

            FollowAndLookCheck();
            UpdateSettings();
            UpdateCameraInputs();
        }

        private void PauseCameraInput(KeyCode keyCode)
        {
            if (keyCode == _inputManager.PlayerInputData.ShowMouse)
            {
                _isPaused = !_isPaused;
                UpdateCameraInputs();
            }
        }

        public void UpdateSettings()
        {
            FreeLookMovement.Follow = Follow;
            FreeLookMovement.LookAt = LookAt;
        }

        public void UpdateCameraInputs()
        {
            if (_isPaused)
            {
                FreeLookMovement.m_YAxis.m_InputAxisName = string.Empty;
                FreeLookMovement.m_YAxis.m_InputAxisValue = 0f;
                FreeLookMovement.m_XAxis.m_InputAxisName = string.Empty;
                FreeLookMovement.m_XAxis.m_InputAxisValue = 0f;
            }
            else
            {
                FreeLookMovement.m_YAxis.m_InputAxisName = _inputManager.PlayerInputData.YAxis;
                FreeLookMovement.m_XAxis.m_InputAxisName = _inputManager.PlayerInputData.XAxis;
            }
        }

        /// <summary>
        /// Auto follow and look at the player if not set
        /// </summary>
        private void FollowAndLookCheck()
        {
            if (Follow == null && LookAt == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementBase>();

                if (player != null)
                {
                    Follow = player.FollowTarget.transform;
                    LookAt = player.LookAt.transform;

                    FreeLookMovement.m_XAxis.m_MaxSpeed = player.InputManager.PlayerInputData.HorizontalSensitivity;
                    FreeLookMovement.m_YAxis.m_MaxSpeed = player.InputManager.PlayerInputData.VerticalSensitivity;
                }
            }
        }
    }
}