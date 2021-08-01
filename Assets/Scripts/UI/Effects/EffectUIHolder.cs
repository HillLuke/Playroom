using Assets.Scripts.Effects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Effects
{
    public class EffectUIHolder : MonoBehaviour
    {
        //TODO: make gamemanager to hold player refrence as a singleton
        public BuffableEntity Player;

        public VerticalLayoutGroup VerticalLayoutGroup;
        public EffectUIData EffectUIDataToInit;

        public Dictionary<EffectBase, EffectUIData> CurrentEffects = new Dictionary<EffectBase, EffectUIData>();

        private void Start()
        {
            Player.ActionEffectAdded += NewEffectAdded;
            Player.ActionEffectRemoved += RemoveEffect;
        }

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
    }
}