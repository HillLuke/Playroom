using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    public class Debuff : EffectBase
    {
        public override EffectType EffectType { get => EffectType.Debuff; }

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