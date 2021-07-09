using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
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

        /// <summary>
        /// Adds an effect - expects an instantiated EffectBase
        /// </summary>
        /// <param name="effect">Expects instantiated</param>
        /// <returns></returns>
        public EffectBase AddEffect(EffectBase effect)
        {
            _effects.Add(effect);
            effect.Initialize(gameObject);
            effect.Activate();

            return effect;
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