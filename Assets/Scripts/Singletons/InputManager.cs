using Assets.Scripts.Player;
using Assets.Scripts.Utilities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Singletons
{
    /// <summary>
    /// Central logic for checking input
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        public Action<InputAction> ActionKeyPressed;
        public Action<InputAction> ActionReMapped;

        public PlayerInputData PlayerInputData;
        public Vector2 MovementVector => _hasControl ? _movment : Vector2.zero;
        public Vector2 CameraVector => _hasControl ? _camera : Vector2.zero;
        public bool Jump => _hasControl ? _jump : false;
        public bool Run => _hasControl ? _run : false;
        public bool UIShift => _uIShift;

        [ReadOnly]
        [ShowInInspector]
        private Vector2 _movment;

        [ReadOnly]
        [ShowInInspector]
        private Vector2 _camera;

        [ReadOnly]
        [ShowInInspector]
        private bool _hasControl;

        [ReadOnly]
        [ShowInInspector]
        private bool _jump;

        [ReadOnly]
        [ShowInInspector]
        private bool _run;

        [ReadOnly]
        [ShowInInspector]
        private bool _isMouseShown = false;

        [ReadOnly]
        [ShowInInspector]
        private bool _uIShift = false;

        private void Update()
        {
            _movment.Set(Input.GetAxisRaw(PlayerInputData.Horizontal), Input.GetAxisRaw(PlayerInputData.Vertical));

            _jump = Input.GetKeyDown(PlayerInputData.Movement_Jump.KeyCode);
            _run = Input.GetKey(PlayerInputData.Movement_Run.KeyCode);
            _uIShift = Input.GetKey(PlayerInputData.UI_Shift.KeyCode);

            CheckActionAndUIInputs();
        }

        private void FixedUpdate()
        {
            if (_hasControl)
            {
                _camera.Set(Input.GetAxis(PlayerInputData.XAxis), Input.GetAxis(PlayerInputData.YAxis));
            }
        }

        protected override void Awake()
        {
            if (PlayerInputData == null)
            {
                Debug.LogError("PlayerInputData is null");
            }
            Setup();
            base.Awake();
        }

        protected override void Setup()
        {
            ActionKeyPressed += KeyPressed;
            ActionKeyPressed += DebugAction;

            HideMouse();
            ReleaseControl();

            base.Setup();
        }

        private void DebugAction(InputAction inputAction)
        {
            Debug.Log("Action " + inputAction.InputType);
        }

        private void KeyPressed(InputAction inputAction)
        {
            if (inputAction.InputType == EInputType.UI_ShowMouse)
            {
                ToggleMouseAndControl();
            }
            if (inputAction.InputType == EInputType.UI_CloseAllUI)
            {
                HideMouse();
                ReleaseControl();
            }
        }

        private void ToggleMouseAndControl()
        {
            if (!_isMouseShown)
            {
                ShowMouse();
                LockControl();
            }
            else
            {
                HideMouse();
                ReleaseControl();
            }
        }

        private void CheckActionAndUIInputs()
        {
            //UI events
            if (Input.GetKeyDown(PlayerInputData.UI_ShowMouse.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.UI_ShowMouse);
            if (Input.GetKeyDown(PlayerInputData.UI_Character.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.UI_Character);
            if (Input.GetKeyDown(PlayerInputData.UI_CloseAll.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.UI_CloseAll);
            if (Input.GetKeyDown(PlayerInputData.UI_Inventory.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.UI_Inventory);

            //Action events
            if (Input.GetKeyDown(PlayerInputData.Action_Use.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.Action_Use);
            if (Input.GetKeyDown(PlayerInputData.Action_DropItem.KeyCode))
                ActionKeyPressed.Invoke(PlayerInputData.Action_DropItem);
        }

        /// <summary>
        /// Locks the user controls
        /// </summary>
        public void LockControl()
        {
            _hasControl = false;
        }

        /// <summary>
        /// Unlocks the user controls
        /// </summary>
        public void ReleaseControl()
        {
            _hasControl = true;
        }

        public void ShowMouse()
        {
            _isMouseShown = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void HideMouse()
        {
            _isMouseShown = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}