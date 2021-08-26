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
        public Action<KeyCode> ActionKeyPressed;

        public PlayerInputData PlayerInputData;
        public Vector2 MovementVector => _hasControl ? _movment : Vector2.zero;
        public Vector2 Camera => _hasControl ? _camera : Vector2.zero;
        public bool Jump => _hasControl ? _jump : false;
        public bool Run => _hasControl ? _run : false;
        public bool ShowMouse => _hasControl ? _showMouse : false;
        public bool LeftClick => _hasControl ? _leftClick : false;
        public bool Interact => _hasControl ? _interact : false;
        public bool DropFirstItem => _hasControl ? _dropFirstItem : false;
        public bool Inventory => _inventory;

        public bool DebugLogging;

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
        private bool _showMouse;

        [ReadOnly]
        [ShowInInspector]
        private bool _leftClick;

        [ReadOnly]
        [ShowInInspector]
        private bool _mouseLocked = true;

        [ReadOnly]
        [ShowInInspector]
        private bool _interact = true;

        [ReadOnly]
        [ShowInInspector]
        private bool _dropFirstItem = true;

        [Title("UI Controls")]
        [ReadOnly]
        [ShowInInspector]
        private bool _inventory = false;

        private void Update()
        {
            _movment.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            _jump = Input.GetKeyDown(PlayerInputData.Jump);
            _run = Input.GetKey(PlayerInputData.Run);
            _showMouse = Input.GetKeyDown(PlayerInputData.ShowMouse);
            _interact = Input.GetKeyDown(PlayerInputData.Use);
            _dropFirstItem = Input.GetKeyDown(PlayerInputData.DropItem);
            _leftClick = Input.GetMouseButton(0);

            _inventory = Input.GetKeyDown(PlayerInputData.Inventory);

            if (ActionKeyPressed != null)
            {
                if (_inventory)
                {
                    ActionKeyPressed.Invoke(PlayerInputData.Inventory);
                }

                if (_showMouse)
                {
                    ActionKeyPressed.Invoke(PlayerInputData.ShowMouse);
                }
            }

            if (_showMouse)
            {
                _mouseLocked = !_mouseLocked;

                if (_mouseLocked)
                {
                    LockControl();
                }
                else
                {
                    ReleaseControl();
                }

                LockMouse();
            }

            if (DebugLogging)
            {
                DebugLog();
            }
        }

        private void FixedUpdate()
        {
            if (_hasControl)
            {
                _camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
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

        /// <summary>
        /// For logging debug data to the console
        /// </summary>
        private void DebugLog()
        {
            //Debug.Log($"Movement - {_movment}");
            Debug.Log($"_mouseLocked - {_mouseLocked}  _hasControl - {_hasControl}");
        }

        protected override void Awake()
        {
            if (PlayerInputData == null)
            {
                Debug.LogError("PlayerInputData is null");
            }

            LockMouse();
            base.Awake();
        }

        private void LockMouse()
        {
            if (_mouseLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ReleaseControl();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                LockControl();
            }
        }
    }
}