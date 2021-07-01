using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    [Serializable]
    public abstract class Debuff : EffectBase
    {
        public override Effect EffectType { get => Effect.Debuff; }

        protected override void ApplyEffect()
        {
            base.ApplyEffect();
        }

        protected override void End()
        {
            base.End();
        }
    }
}