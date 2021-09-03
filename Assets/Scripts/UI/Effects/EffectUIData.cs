using Assets.Scripts.Effects;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Effects
{
    public class EffectUIData : MonoBehaviour
    {
        private EffectBase _effect;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _duration;

        public void Init(EffectBase effect)
        {
            _effect = effect;
            _title.text = _effect.EffectName;
            if (_effect.IsPermanent)
            {
                _duration.enabled = false;
            }
        }

        public void Remove()
        {
            if (_effect != null)
            {
                _effect.End();
            }
        }

        private void Update()
        {
            if (_effect != null && !_effect.IsPermanent)
            {
                _duration.text = _effect.Duration.ToString("0.0");
            }
        }
    }
}