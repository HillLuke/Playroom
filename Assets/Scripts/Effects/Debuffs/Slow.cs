using Assets.Scripts.Player.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    [CreateAssetMenu(fileName = "Debuff", menuName = "Effects/Debuff/Slow")]
    public class Slow : Debuff
    {
        public float SpeedModifier;

        private MovementConfig _movementconfig;

        protected override void ApplyEffect()
        {
            base.ApplyEffect();

            _movementconfig = _target.GetComponent<MovementConfig>();

            if (_movementconfig != null)
            {
                _movementconfig.WalkingSpeedModifier = SpeedModifier;
                _movementconfig.RunningSpeedModifier = SpeedModifier;
            }
        }

        protected override void End()
        {
            base.End();

            if (_movementconfig != null)
            {
                _movementconfig.WalkingSpeedModifier = 0;
                _movementconfig.RunningSpeedModifier = 0;
            }
        }
    }
}