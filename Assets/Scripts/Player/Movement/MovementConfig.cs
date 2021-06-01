using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementConfig : MonoBehaviour
{
    public float WalkingSpeed => _walkingSpeed;
    public float RunningSpeed => _runningSpeed;
    public bool IsRunning { get; set; }
    public bool IsJumping { get; set; }
    public bool IsGrounded { get; set; }
    public bool CanJump => _jumpCount > 0;
    public int MaxJumps => _maxJumps;
    public int JumpsLeft => _maxJumps - _jumpCount;

    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private int _maxJumps;
    [SerializeField] private bool _isRunning;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isJumping;
    [SerializeField] private int _jumpCount;

    public void Init()
    {

    }

    public void Jump()
    {
        if (CanJump)
        {
            _jumpCount++;
            IsJumping = true;
            IsGrounded = false;
        }
    }

    public void Land()
    {
        _jumpCount = 0;
        IsJumping = false;
        IsGrounded = true;
    }

}
