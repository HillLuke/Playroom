using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player input
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public Vector2 Movement => _hasControl ? _movment : Vector2.zero;
    public Vector2 Camera => _hasControl ? _camera : Vector2.zero;
    public bool Jump => _hasControl ? _jump : false;

    private Vector2 _movment;
    private Vector2 _camera;
    private bool _hasControl;
    private bool _jump;

    private void Update()
    {
        _movment.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _jump = Input.GetButton("Jump");
    }

    public void LockControl()
    {
        _hasControl = false;
    }

    public void ReleaseControl()
    {
        _hasControl = true;
    }
}
