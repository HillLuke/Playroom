using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : StatusEffectBase
{
    public override string StatusEffectName => "Slide";

    public override void Apply()
    {
        var MovementConfig = Target.GetComponent<MovementConfig>();

        if (MovementConfig != null)
        {
            MovementConfig.MovementSharpness = 0.5f;
        }
        else
        {
            Remove();
        }
    }

    public override void Remove()
    {
        var MovementConfig = Target.GetComponent<MovementConfig>();

        if (MovementConfig != null)
        {
            MovementConfig.MovementSharpness = 0f;
        }

        base.Remove();
    }
}
