using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// Handles player input
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputData PlayerInputData;

        public Vector2 MovementVector => _hasControl ? _movment : Vector2.zero;
        public Vector2 Camera => _hasControl ? _camera : Vector2.zero;
        public bool Jump => _hasControl ? _jump : false;
        public bool Run => _hasControl ? _run : false;
        public bool ShowMouse => _hasControl ? _showMouse : false;

        public bool DebugLogging;

        private Vector2 _movment;
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
        private bool _mouseLocked = true;        

        private void Update()
        {            
            _movment.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            _jump = Input.GetKeyDown(PlayerInputData.Jump);
            _run = Input.GetKey(PlayerInputData.Run);
            _showMouse = Input.GetKeyDown(PlayerInputData.ShowMouse);

            if (_showMouse)
            {
                _mouseLocked = !_mouseLocked;
                LockMouse();
            }

            if (DebugLogging)
            {
                DebugLog();
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
            Debug.Log($"Movement - {_movment}");
        }

        private void Awake()
        {
            if (PlayerInputData == null)
            {
                Debug.LogError("PlayerInputData is null");
            }

            LockMouse();
        }

        private void LockMouse()
        {
            if (_mouseLocked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}