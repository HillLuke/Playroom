using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class EffectBase : ScriptableObject
    {
        public string EffectName;
        public string Description;
        public float Duration;   
        public bool IsDurationStacked;
        public bool IsEffectStacked;

        public int Stacks { get { return _stacks; } }
        public bool IsActive { get {return _isActive; } }
        public bool IsFinished { get { return _isFinished; } }
        public abstract EffectType EffectType { get; }

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
            Duration -= delta;
            if (Duration <= 0)
            {
                End();
            }
        }

        public virtual void Initialize(GameObject Target) { _target = Target; }
        protected virtual void ApplyEffect() { }
        protected virtual void End() 
        {
            _isActive = false;
            _isFinished = true;
        }
    }

    public enum EffectType
    {
        Buff,
        Debuff,
        Passive
    }
}