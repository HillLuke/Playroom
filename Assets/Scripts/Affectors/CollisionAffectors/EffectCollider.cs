using Assets.Scripts.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Affectors.CollisionAffectors
{
    [Serializable]
    [RequireComponent(typeof(Collider))]
    public class EffectCollider : MonoBehaviour
    {
        [SerializeReference]
        public EffectBase Effect;

        public bool RemoveOnExit;
        public bool ReApplyOnStay;

        [SerializeReference]
        public Dictionary<BuffableEntity, EffectBase> Entities;

        private void Awake()
        {
            Entities = new Dictionary<BuffableEntity, EffectBase>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var BuffableEntity = other.GetComponent<BuffableEntity>();

            if (BuffableEntity != null)
            {
                var effect = Effect.Copy();

                if (!Entities.ContainsKey(BuffableEntity) && RemoveOnExit)
                {
                    Entities.Add(BuffableEntity, effect);
                }

                BuffableEntity.AddEffect(effect);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (RemoveOnExit)
            {
                var BuffableEntity = other.GetComponent<BuffableEntity>();

                if (Entities.ContainsKey(BuffableEntity))
                {
                    Entities[BuffableEntity].End();
                    Entities.Remove(BuffableEntity);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {            
            if (ReApplyOnStay)
            {
                var BuffableEntity = other.GetComponent<BuffableEntity>();

                //todo: investigate memory managment for removing from the effect list
                if (Entities.ContainsKey(BuffableEntity) && Entities[BuffableEntity].IsFinished)
                {
                    var effect = Effect.Copy();
                    Entities[BuffableEntity] = effect;
                    BuffableEntity.AddEffect(effect);
                }
            }
        }
    }
}