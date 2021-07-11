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


        public event Action<EffectBase> ActionEffectAdded;
        public event Action<EffectBase> ActionEffectRemoved;

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

            if (ActionEffectAdded != null)
            {
                ActionEffectAdded.Invoke(effect);
            }
            return effect;
        }

        private void Update()
        {
            foreach (var effect in _effects.Where(x => x.IsActive))
            {
                effect.Tick(Time.deltaTime);
            }

            foreach (var effect in _effects.Where(x => !x.IsActive).ToList())
            {
                if (ActionEffectRemoved != null)
                {
                    ActionEffectRemoved.Invoke(effect);
                }
                _effects.Remove(effect);
            }
        }

        private void Awake()
        {
            _effects = new List<EffectBase>();
        }
    }
}