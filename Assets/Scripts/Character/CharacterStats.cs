using Sirenix.OdinInspector;
using UnityEngine;
using static Assets.Scripts.Character.CharacterProperties.Property;

namespace Assets.Scripts.Character
{
    public class CharacterStats : MonoBehaviour
    {
        public CharacterProperties CharacterProperties;

        [ReadOnly]
        [ShowInInspector]
        public bool IsGrounded { get; set; }

        [ReadOnly]
        [ShowInInspector]
        public bool IsJumping { get; set; }

        [ReadOnly]
        [ShowInInspector]
        public bool CanJump { get; set; }

        /// <summary>
        /// e.g. walking on ice, closer to 0 the more movment slides
        /// </summary>
        public float MovementSharpness => Property(PropertyType.MovementSharpness);

        public float WalkingSpeed => Property(PropertyType.Speed) * Property(PropertyType.WalkingSpeedMultiplier);
        public float RunningSpeed => Property(PropertyType.Speed) * Property(PropertyType.RunningSpeedMultiplier);
        public float JumpForce => Property(PropertyType.JumpForce);
        public float Gravity => Property(PropertyType.Gravity);
        public int JumpsLeft => (int)Property(PropertyType.MaxJumps) - _jumpsLeft;

        [SerializeField] private int _jumpsLeft;

        [Title("Properties")]
        [SerializeField] private CharacterProperties _characterProperties;

        private void Awake()
        {
            try
            {
                CharacterProperties = Instantiate(_characterProperties);
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        private float Property(PropertyType Type)
        {
            return CharacterProperties.GetPropertyFinalValue(Type);
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
    }
}