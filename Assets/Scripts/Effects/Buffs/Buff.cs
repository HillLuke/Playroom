using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Buffs
{
    public abstract class Buff : EffectBase
    {
        public override Effect EffectType { get => Effect.Buff; }

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