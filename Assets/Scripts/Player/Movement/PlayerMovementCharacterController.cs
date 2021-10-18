using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementCharacterController : PlayerMovementBase
    {
        private CharacterController _characterController;
        [SerializeField] private Vector3 _playerVelocity;

        protected override void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            _characterController.Move(_playerVelocity * Time.deltaTime);
            _movementData.IsGrounded = _characterController.isGrounded;

            if (_movementData.IsGrounded && _movementData.IsJumping)
            {
                _movementData.ResetJumps();
            }

            UpdateOrientation();
        }


        protected override void CalculateJump()
        {
            _playerVelocity.y = _movementData.JumpForce;

            _movementData.Jump();
        }

        protected override void CalculateMove()
        {
            //Get worldspace input to handle movment based off of current rotation
            var moveinput = new Vector3(_inputManager.MovementVector.x, 0, _inputManager.MovementVector.y);
            moveinput = Vector3.ClampMagnitude(moveinput, 1);
            Vector3 worldspaceMoveInput = transform.TransformVector(moveinput);
            Vector3 targetVelocity = worldspaceMoveInput * (_inputManager.Run ? _movementData.RunningSpeed : _movementData.WalkingSpeed);

            if (_movementData.MovementSharpness <= 0)
            {
                _playerVelocity.x = targetVelocity.x;
                _playerVelocity.z = targetVelocity.z;
            }
            else
            {
                //Moving on slidy surface
                var moveVector = Vector3.Lerp(_playerVelocity, targetVelocity, _movementData.MovementSharpness * Time.deltaTime);
                _playerVelocity.x = moveVector.x;
                _playerVelocity.z = moveVector.z;
            }

            if (_movementData.IsGrounded)
            {
                if (_playerVelocity.y <= 0)
                {
                    _playerVelocity.y = -0.2f; //Little bit of gravity to stick to the ground;
                }
            }
            else
            {
                if (_playerVelocity.y < 0)
                {
                    _playerVelocity.y += _movementData.Gravity * 4f * Time.deltaTime;
                }
                else if (_playerVelocity.y > 0 && _playerVelocity.y < (_movementData.JumpForce / 2))
                {
                    _playerVelocity.y += _movementData.Gravity * 2f * Time.deltaTime;
                }
                else
                {
                    _playerVelocity.y += _movementData.Gravity * 1f * Time.deltaTime;
                }
            }
        }

        private void UpdateOrientation()
        {
            var rotInput = new Vector2(_inputManager.CameraVector.x, _inputManager.CameraVector.y);
            var rot = transform.eulerAngles;
            rot.y += rotInput.x * (_inputManager.PlayerInputData.HorizontalSensitivity * _inputManager.PlayerInputData.HorizontalBase);
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}