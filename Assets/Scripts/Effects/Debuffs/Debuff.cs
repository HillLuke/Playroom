using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    public class Debuff : EffectBase
    {
        protected override EffectType EffectType { get => EffectType.Debuff; }
    }
}