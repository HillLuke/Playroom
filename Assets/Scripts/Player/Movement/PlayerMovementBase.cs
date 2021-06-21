using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
[RequireComponent(typeof(MovementConfig))]
public abstract class PlayerMovementBase : MonoBehaviour
{
    public bool DebugLogging;
    // Where the camera will centre on
    public GameObject LookAt;

    protected MovementConfig _movementData;
    protected PlayerInput _playerInput;
    protected UnityEngine.Camera _camera;

    protected virtual void Start()
    {
    }

    protected virtual void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementData = GetComponent<MovementConfig>();
        _camera = UnityEngine.Camera.main;

        _movementData.Init();
        _playerInput.ReleaseControl();
    }

    protected virtual void FixedUpdate()
    {
        CalculateMove();
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
            CalculateJump();
        }
    }

    protected abstract void CalculateJump();

    protected abstract void CalculateMove();
}
}