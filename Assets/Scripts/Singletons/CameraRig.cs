using Assets.Scripts.Player;
using Assets.Scripts.Singletons;
using Assets.Scripts.Utilities;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraRig : Singleton<CameraRig>
    {
        public CinemachineFreeLook FreeLookMovement;
        public CinemachineBrain Brain;

        private InputManager _inputManager;
        private PlayerManager _playerManager;
        private bool _isPaused = false;
        private Transform _follow;
        private Transform _lookAt;

        protected override void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;

                _inputManager.ActionKeyPressed += KeyPressed;
            }

            if (PlayerManager.instanceExists)
            {
                _playerManager = PlayerManager.instance;

                _playerManager.ActionPlayerChanged += Initialize;
            }

            Setup();
            base.Start();
        }

        protected override void Setup()
        {
            if (_inputManager.isSetup && _playerManager.isSetup)
            {
                Initialize(_playerManager.Player);
                base.Setup();
            }
            else
            {
                _inputManager.ActionSetup += Setup;
                _playerManager.ActionSetup += Setup;
            }
        }

        public void PauseCamera()
        {
            _isPaused = true;
            FreeLookMovement.m_YAxis.m_InputAxisName = string.Empty;
            FreeLookMovement.m_YAxis.m_InputAxisValue = 0f;
            FreeLookMovement.m_XAxis.m_InputAxisName = string.Empty;
            FreeLookMovement.m_XAxis.m_InputAxisValue = 0f;
        }

        public void ResumeCamera()
        {
            _isPaused = false;
            FreeLookMovement.m_YAxis.m_InputAxisName = _inputManager.PlayerInputData.YAxis;
            FreeLookMovement.m_XAxis.m_InputAxisName = _inputManager.PlayerInputData.XAxis;
        }

        public void ConfigCamera()
        {
            FreeLookMovement.Follow = _follow;
            FreeLookMovement.LookAt = _lookAt;

            FreeLookMovement.m_YAxis.m_MaxValue = 90f;

            FreeLookMovement.m_XAxis.m_MaxSpeed = _inputManager.PlayerInputData.HorizontalSensitivity;
            FreeLookMovement.m_YAxis.m_MaxSpeed = _inputManager.PlayerInputData.VerticalSensitivity;

            FreeLookMovement.m_XAxis.Value = 0f;
            FreeLookMovement.m_YAxis.Value = 50f;
        }

        private void Initialize(PlayerController player)
        {
            PauseCamera();
            _follow = player.GetComponent<PlayerCameraConfig>().Follow.transform;
            _lookAt = player.GetComponent<PlayerCameraConfig>().LookAt.transform;
            ConfigCamera();
            ResumeCamera();
        }

        private void KeyPressed(InputAction inputAction)
        {
            if (inputAction.InputType == EInputType.UI_ShowMouse)
            {
                TogglePause();
            }
        }

        private void TogglePause()
        {
            if (_isPaused)
            {
                ResumeCamera();
            }
            else
            {
                PauseCamera();
            }
        }
    }
}