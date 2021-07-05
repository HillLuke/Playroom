using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Base class for status effects (buffs, debuffs, passives)
    /// Inheritance allows for more customizable effects such as using the same effect in multiple places with difference strengths
    /// Striptable Object may be better in instances where effect data never changes e.g. Fire damage will always do 5hp damage
    /// </summary>
    [Serializable]
    public abstract class EffectBase
    {
        [Header("Effect Base Settings")]
        public string EffectName;
        public string Description;
        public float Duration;
        public bool IsDurationStacked;
        public bool IsEffectStacked;
                       
        [ShowInInspector]
        public abstract Effect Effect { get; }
        [ShowInInspector]
        public abstract EffectType EffectType { get; }
        [ShowInInspector]
        public abstract EffectBehaviour EffectBehaviour { get; }

        public int Stacks { get { return _stacks; } }
        public bool IsActive { get {return _isActive; } }
        public bool IsFinished { get { return _isFinished; } }

        [SerializeField]
        private protected bool _isActive;
        [SerializeField]
        private protected bool _isFinished;
        [SerializeField]
        private protected int _stacks;
        [SerializeField]
        private protected GameObject _target;

        public void Activate()
        {
            _isActive = true;
            _isFinished = false;

            if (IsEffectStacked)
            {
                _stacks++;
            }
            else
            {
                _stacks = 1;
            }

            if (IsDurationStacked)
            {
                Duration += Duration;
            }

            ApplyEffect();
        }

        public void Tick(float delta)
        {
            Duration -= delta;
            if (Duration <= 0)
            {
                End();
            }
        }

        public virtual void Initialize(GameObject Target) { _target = Target; }
        protected virtual void ApplyEffect() { }
        public virtual void End() 
        {
            _isActive = false;
            _isFinished = true;
        }

        public abstract EffectBase Copy();
    }


    public enum Effect
    {
        Buff,
        Debuff,
        Passive
    }

    public enum EffectBehaviour
    {
        DamageOverTime,
        HealingOverTime,
        CrowdControl,
        StatChange
    }

    public enum EffectType
    {
        None,
        Disease,
        Poison,
        Bleed,
        Fire,
        Holy,
        Aura
    }
}