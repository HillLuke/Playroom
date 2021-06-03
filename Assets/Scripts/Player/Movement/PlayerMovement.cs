using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementConfig))]
public abstract class PlayerMovement : MonoBehaviour
{
    public bool DebugLogging;

    protected MovementConfig _movementData;
    protected PlayerInput _playerInput;

    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementData = GetComponent<MovementConfig>();

        _movementData.Init();
        _playerInput.ReleaseControl();
    }

    protected virtual void FixedUpdate()
    {
        Move(_playerInput.MovementVector);
    }

    protected virtual void Update()
    {
        if (_playerInput.Jump && _movementData.JumpsLeft > 0 && !_movementData.CanJump)
        {
            _movementData.CanJump = true;
        }

        if (_movementData.CanJump)
        {
            _movementData.CanJump = false;
            Jump();
        }
    }

    /// <summary>
    ///Makes the player jump
    /// </summary>
    protected abstract void Jump();

    /// <summary>
    /// Move the player
    /// </summary>
    /// <param name="MovementVector">Input vector for directional movement</param>
    protected abstract void Move(Vector2 MovementVector);
}
