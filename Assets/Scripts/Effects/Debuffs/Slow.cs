using Assets.Scripts.Player.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    [Serializable]
    public class Slow : Debuff
    {
        [Header("Slow Settings")]
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

        public override EffectBase Copy()
        {
            return new Slow
            {
                EffectName = EffectName,
                Description = Description,
                IsDurationStacked = IsDurationStacked,
                IsEffectStacked = IsEffectStacked,
                Duration = Duration,
                SpeedModifier = SpeedModifier
            };
        }
    }
}