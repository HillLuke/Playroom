using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Affectors.CollisionAffectors
{
public class StatusEffectCollider : CollisionAffectorBase
{
    public StatusEffectBase statusEffect;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.AddComponent(statusEffect.GetType());
        var t = (StatusEffectBase)other.gameObject.GetComponent(statusEffect.GetType());

        if (t != null)
        {
            t.Target = other.gameObject;
            t.Apply();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var t = (StatusEffectBase)other.gameObject.GetComponent(statusEffect.GetType());

        if (t != null)
        {
            t.Remove();
        }
    }
}
}