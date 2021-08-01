using UnityEngine;

namespace Assets.Scripts.Effects.Buffs
{
    [CreateAssetMenu(fileName = "Buff", menuName = "Effect/Buff")]
    public class Buff : EffectBase
    {
        public override Effect Effect { get => Effect.Buff; }

        public override EffectType EffectType { get; set; }

        public override EffectBehaviour EffectBehaviour { get; set; }

        protected override void ApplyEffect()
        {
            base.ApplyEffect();
        }

        public override void End()
        {
            base.End();
        }
    }
}