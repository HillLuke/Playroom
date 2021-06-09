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

        UpdateOrientation();

        _characterController.Move(_playerVelocity * Time.deltaTime);
        _movementData.IsGrounded = _characterController.isGrounded;

        if (_movementData.IsGrounded && _movementData.IsJumping)
        {
            _movementData.ResetJumps();
        }
    }

    protected override void CalculateJump()
    {
        _playerVelocity.y = _movementData.JumpForce;

        _movementData.Jump();
    }

    protected override void CalculateMove()
    {
        //Get worldspace input to handle movment based off of current rotation
        var moveinput = new Vector3(_playerInput.MovementVector.x, 0, _playerInput.MovementVector.y);
        moveinput = Vector3.ClampMagnitude(moveinput, 1);
        Vector3 worldspaceMoveInput = transform.TransformVector(moveinput);
        Vector3 targetVelocity = worldspaceMoveInput * (_playerInput.Run ? _movementData.RunningSpeed : _movementData.WalkingSpeed);

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
    
    void UpdateOrientation()
    {
        //Make the player always face forward
        float yaw = _camera.transform.rotation.eulerAngles.y;
        //Use 1 in Slerp to make rotation instant
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yaw, 0), 1);
    }
}
