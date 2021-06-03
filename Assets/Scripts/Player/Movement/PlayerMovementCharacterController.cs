using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementCharacterController : PlayerMovement
{
    private CharacterController _characterController;
    [SerializeField] private Vector3 _playerVelocity;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();       
    }

    protected override void Update()
    {
        base.Update();
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
    }

    protected override void Jump()
    {
        _playerVelocity.y = _movementData.JumpForce;

        _movementData.Jump();
    }

    protected override void Move(Vector2 MovementVector)
    {
        if (_movementData.MovementSharpness == 0)
        {
            var moveinput = new Vector3(MovementVector.x, 0, MovementVector.y);
            var moveVector = moveinput * (_playerInput.Run ? _movementData.RunningSpeed : _movementData.WalkingSpeed);
            _playerVelocity.x = moveVector.x;
            _playerVelocity.z = moveVector.z;
        }
        else
        {
            //Moving on slidy surface 
            var moveinput = new Vector3(MovementVector.x, 0, MovementVector.y);
            moveinput = Vector3.ClampMagnitude(moveinput, 1);
            Vector3 worldspaceMoveInput = transform.TransformVector(moveinput);

            Vector3 targetVelocity = worldspaceMoveInput * (_playerInput.Run ? _movementData.RunningSpeed : _movementData.WalkingSpeed);
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
}
