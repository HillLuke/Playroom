using UnityEngine;

namespace Assets.Scripts.Effects.Debuffs
{
    [CreateAssetMenu(fileName = "Debuff", menuName = "Effect/Debuff")]
    public class Debuff : EffectBase
    {
        public override Effect Effect { get => Effect.Debuff; }

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