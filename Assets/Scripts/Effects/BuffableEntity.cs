using Assets.Scripts.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    [Serializable]
    public class BuffableEntity : MonoBehaviour
    {
        public List<EffectBase> Effects { get { return _effects; } }

        [SerializeReference]
        private List<EffectBase> _effects;

        public void AddEffect(EffectBase effect)
        {
            _effects.Add(effect);
            effect.Initialize(gameObject);
            effect.Activate();
        }

        private void Update()
        {
            foreach (var effect in _effects.Where(x => x.IsActive))
            {
                effect.Tick(Time.deltaTime);
            }

            _effects.RemoveAll(x => x.IsFinished);
        }

        private void Awake()
        {
            _effects = new List<EffectBase>();
        }
    }
}