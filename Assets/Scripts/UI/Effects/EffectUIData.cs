using Assets.Scripts.Effects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectUIData : MonoBehaviour
{
    private EffectBase _effect;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _duration;

    public void Init(EffectBase effect)
    {
        _effect = effect;
        _title.text = _effect.EffectName;
        if (_effect.IsPermanent)
        {
            _duration.enabled = false;
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
