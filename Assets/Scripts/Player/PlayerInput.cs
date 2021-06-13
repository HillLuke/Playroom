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
    public Vector2 MovementVector => _hasControl ? _movment : Vector2.zero;
    public Vector2 Camera => _hasControl ? _camera : Vector2.zero;
    public bool Jump => _hasControl ? (_jump) : false;
    public bool Run => _hasControl ? _run : false;

    public bool DebugLogging;

    private Vector2 _movment;
    private Vector2 _camera;
    private bool _hasControl;
    private bool _jump;
    private bool _run;

    private void Update()
    {
        _movment.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _jump = Input.GetButtonDown("Jump");
        _run = Input.GetKey(KeyCode.LeftShift);

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
}
}