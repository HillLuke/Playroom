using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraRig : MonoBehaviour
    {
        public CinemachineFreeLook FreeLookMovement;
        public CinemachineBrain Brain;

        private InputManager _inputManager;
        private PlayerManager _playerManager;
        private bool _isPaused = false;
        private Transform _follow;
        private Transform _lookAt;

        private void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += PauseCameraInput;
            }

            if (PlayerManager.instanceExists)
            {
                _playerManager = PlayerManager.instance;

                _playerManager.ActionPlayerChanged += Initialize;

                if (_playerManager.Player != null)
                {
                    Initialize(_playerManager.Player);
                }
            }
        }

        private void Initialize(PlayerController player)
        {
            _follow = player.GetComponent<PlayerCameraConfig>().Follow.transform;
            _lookAt = player.GetComponent<PlayerCameraConfig>().LookAt.transform;

            FreeLookMovement.Follow = _follow;
            FreeLookMovement.LookAt = _lookAt;

            FreeLookMovement.m_YAxis.m_MaxValue = 90f;

            FreeLookMovement.m_XAxis.m_MaxSpeed = _inputManager.PlayerInputData.HorizontalSensitivity;
            FreeLookMovement.m_YAxis.m_MaxSpeed = _inputManager.PlayerInputData.VerticalSensitivity;

            FreeLookMovement.m_YAxis.Value = 45f;

            UpdateCameraInputs();
        }

        private void PauseCameraInput(InputAction inputAction)
        {
            if (inputAction.InputType != EInputType.UI_ShowMouse)
            {
                return;
            }

            _isPaused = !_isPaused;
            UpdateCameraInputs();
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
    }
}