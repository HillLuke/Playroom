using Assets.Scripts.Effects;
using Assets.Scripts.Effects.Debuffs;
using UnityEngine;

namespace Assets.Scripts.Affectors.CollisionAffectors
{
    [RequireComponent(typeof(Collider))]
    public abstract class CollisionAffectorBase : MonoBehaviour
    {
        public EffectBase Effect;

        protected virtual void Start() {}

        protected virtual void Update() { }
    }
}