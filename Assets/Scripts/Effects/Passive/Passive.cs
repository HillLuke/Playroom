using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Passive
{
    public abstract class Passive : EffectBase
    {
        public override Effect EffectType { get => Effect.Passive; }

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