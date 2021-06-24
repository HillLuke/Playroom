using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class BuffableEntity : MonoBehaviour
    {
        public List<EffectBase> Effects { get { return _effects; } }

        [SerializeField]
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
                if (effect.IsFinished)
                {
                    _effects.Remove(effect);
                }
            }
        }

        private void Awake()
        {
            _effects = new List<EffectBase>();
        }
    }
}