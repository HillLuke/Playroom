using Assets.Scripts.Character;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using static Assets.Scripts.Character.CharacterProperties;
using static Assets.Scripts.Character.CharacterProperties.Property;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Base class for status effects (buffs, debuffs, passives)
    /// Inheritance allows for more customizable effects such as using the same effect in multiple places with difference strengths
    /// Striptable Object may be better in instances where effect data never changes e.g. Fire damage will always do 5hp damage
    /// </summary>
    [Serializable]
    public abstract class EffectBase : ScriptableObject
    {
        [Title("Base data")]
        public string EffectName;

        public string Description;
        public float Duration;
        public bool IsPermanent;
        public bool IsDurationStacked;
        public bool IsEffectStacked;
        public PropertyType ApplyTo;
        public Modifier Modifier;

        [Title("Show in inspector fields")]
        [ShowInInspector]
        public abstract Effect Effect { get; }

        [ShowInInspector]
        public abstract EffectType EffectType { get; set; }

        [ShowInInspector]
        public abstract EffectBehaviour EffectBehaviour { get; set; }

        public int Stacks { get { return _stacks; } }

        [ShowInInspector]
        public bool IsActive { get { return _isActive; } }

        [ShowInInspector]
        public bool IsFinished { get { return _isFinished; } }

        private protected bool _isActive;
        private protected bool _isFinished;
        private protected int _stacks;
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
            if (!IsPermanent)
            {
                Duration -= delta;
                if (Duration <= 0)
                {
                    End();
                }
            }
        }

        public virtual void Initialize(GameObject Target)
        {
            _target = Target;
        }

        protected virtual void ApplyEffect()
        {
            var characterProperties = _target.GetComponent<CharacterStats>().CharacterProperties;
            characterProperties.ApplyModifierToProperty(ApplyTo, Modifier);
        }

        public virtual void End()
        {
            _isActive = false;
            _isFinished = true;

            var characterProperties = _target.GetComponent<CharacterStats>().CharacterProperties;
            characterProperties.RemoveModifierFromProperty(ApplyTo, Modifier);
        }
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