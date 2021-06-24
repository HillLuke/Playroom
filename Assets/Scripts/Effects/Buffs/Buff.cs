using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Buffs
{
    public class Buff : EffectBase
    {
        public override EffectType EffectType { get => EffectType.Buff; }

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