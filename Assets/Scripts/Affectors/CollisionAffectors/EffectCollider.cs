using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Affectors.CollisionAffectors
{
    public class EffectCollider : CollisionAffectorBase
    {
        private void OnTriggerEnter(Collider other)
        {
            var BuffableEntity = other.GetComponent<BuffableEntity>();

            if (BuffableEntity != null)
            {
                BuffableEntity.effects.Add(Effect);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var BuffableEntity = other.GetComponent<BuffableEntity>();

            if (BuffableEntity != null)
            {

            }
        }
    }
}