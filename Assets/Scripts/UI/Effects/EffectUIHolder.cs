using Assets.Scripts.Effects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Effects
{
    public class EffectUIHolder : UIBase
    {
        public VerticalLayoutGroup VerticalLayoutGroup;
        public EffectUIData EffectUIDataToInit;

        public Dictionary<EffectBase, EffectUIData> CurrentEffects = new Dictionary<EffectBase, EffectUIData>();

        private BuffableEntity _buffableEntity;

        private void NewEffectAdded(EffectBase effectBase)
        {
            var effectUIData = Instantiate(EffectUIDataToInit, VerticalLayoutGroup.transform);
            effectUIData.Init(effectBase);
            CurrentEffects.Add(effectBase, effectUIData);
        }

        private void RemoveEffect(EffectBase effectBase)
        {
            if (CurrentEffects.ContainsKey(effectBase))
            {
                Destroy(CurrentEffects[effectBase].gameObject);
                CurrentEffects.Remove(effectBase);
            }
        }

        protected override void Setup()
        {
            _buffableEntity = _activePlayer.GetComponent<BuffableEntity>();

            foreach (var effect in CurrentEffects)
            {
                effect.Value.Remove();
            }

            CurrentEffects = new Dictionary<EffectBase, EffectUIData>();

            if (_buffableEntity != null)
            {
                _buffableEntity.ActionEffectAdded += NewEffectAdded;
                _buffableEntity.ActionEffectRemoved += RemoveEffect;
            }
            else
            {
                Debug.LogError("_buffableEntity is null");
            }

            base.Setup();
        }
    }
}