using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.Passive
{
    public class Passive : EffectBase
    {
        protected override EffectType EffectType { get => EffectType.Passive; }
    }
}