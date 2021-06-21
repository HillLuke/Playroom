using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class EffectBase : MonoBehaviour
    {
        public string EffectName;
        protected abstract EffectType EffectType { get; }
    }

    public enum EffectType
    {
        Buff,
        Debuff,
        Passive
    }
}