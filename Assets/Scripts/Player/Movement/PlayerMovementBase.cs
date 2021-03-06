using Assets.Scripts.Character;
using Assets.Scripts.Singletons;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    public abstract class PlayerMovementBase : MonoBehaviour
    {
        public bool DebugLogging;

        // Where the camera will centre on
        public GameObject LookAt;
        public GameObject FollowTarget;

        protected CharacterStats _movementData;
        protected InputManager _inputManager;

        protected virtual void Start()
        {
            if (InputManager.instanceExists)
            {
                _inputManager = InputManager.instance;
            }

            _movementData = GetComponent<CharacterStats>();
            _inputManager.ReleaseControl();
        }

        protected virtual void Awake()
        {
        }

        protected virtual void FixedUpdate()
        {
            CalculateMove();
        }

        protected virtual void Update()
        {
            if (_inputManager.Jump && _movementData.JumpsLeft > 0 && !_movementData.CanJump)
            {
                _movementData.CanJump = true;
            }

            if (_movementData.CanJump)
            {
                _movementData.CanJump = false;
                CalculateJump();
            }
        }

        protected abstract void CalculateJump();

        protected abstract void CalculateMove();
    }
}