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

        private EffectBase _effect;

        private void Awake()
        {
            Entities = new Dictionary<BuffableEntity, EffectBase>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var BuffableEntity = other.GetComponent<BuffableEntity>();

            if (BuffableEntity != null)
            {
                if (!Entities.ContainsKey(BuffableEntity))
                {
                    _effect = Instantiate(Effect);
                    Entities.Add(BuffableEntity, _effect);
                    BuffableEntity.AddEffect(_effect);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("collision");
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
                    _effect = Instantiate(Effect);
                    Entities[BuffableEntity] = _effect;
                    BuffableEntity.AddEffect(_effect);
                }
            }
        }
    }
}