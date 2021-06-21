using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Buffs
{
    public class Buff : EffectBase
    {
        protected override EffectType EffectType { get => EffectType.Buff; }
    }
}