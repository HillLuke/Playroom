using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Passive
{
    public abstract class Passive : EffectBase
    {
        public override Effect Effect { get => Effect.Passive; }

        protected override void ApplyEffect()
        {
            base.ApplyEffect();
        }

        public override void End()
        {
            base.End();
        }
    }
}