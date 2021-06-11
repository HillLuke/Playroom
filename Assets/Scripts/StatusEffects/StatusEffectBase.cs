using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectBase : MonoBehaviour
{
    public GameObject Target;

    public abstract string StatusEffectName { get; }

    public abstract void Apply();
    public virtual void Remove()
    {
        Destroy(GetComponent<StatusEffectBase>());
    }
}
