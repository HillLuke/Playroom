using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Movement
{
    public class MovementConfig : MonoBehaviour
    {
        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }
        public bool CanJump { get; set; }
        /// <summary>
        /// e.g. walking on ice, closer to 0 the more movment slides
        /// </summary>
        public float MovementSharpness { get { return _movementSharpness; } set { _movementSharpness = value; } }
        public float WalkingSpeedModifier { get { return _walkingSpeedModifier; } set { _walkingSpeedModifier = value; } }
        public float RunningSpeedModifier { get { return _runningSpeedModifier; } set { _runningSpeedModifier = value; } }
        public float WalkingSpeed => WalkingSpeedModifier == 0 ? _walkingSpeed : _walkingSpeed * WalkingSpeedModifier;
        public float RunningSpeed => RunningSpeedModifier == 0 ? _runningSpeed : _runningSpeed * RunningSpeedModifier;
        public float JumpForce => _jumpForce;
        public float Gravity => Physics.gravity.y;
        public int MaxJumps => _maxJumps;
        public int JumpsLeft => _maxJumps - _jumpsLeft;

        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _runningSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _movementSharpness;
        [SerializeField] private float _walkingSpeedModifier;
        [SerializeField] private float _runningSpeedModifier;
        [SerializeField] private int _maxJumps;
        [SerializeField] private int _jumpsLeft;

        public void Init()
        {
            _walkingSpeedModifier = 0f;
            _runningSpeedModifier = 0f;
        }

        public void Jump()
        {
            _jumpsLeft++;
            IsJumping = true;
            IsGrounded = false;
        }

        public void ResetJumps()
        {
            _jumpsLeft = 0;
            IsJumping = false;
        }

        public void IncreaseMaxJumps(int additionalJumps)
        {
            _maxJumps += additionalJumps;
        }

    }
}