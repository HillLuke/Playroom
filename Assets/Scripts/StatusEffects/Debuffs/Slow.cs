using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : StatusEffectBase
{
    public override string StatusEffectName => "Slow";

    public override void Apply()
    {
        var MovementConfig = Target.GetComponent<MovementConfig>();

        if (MovementConfig != null)
        {
            MovementConfig.WalkingSpeedModifier = 0.5f;
            MovementConfig.RunningSpeedModifier = 0.5f;
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
            MovementConfig.WalkingSpeedModifier = 0f;
            MovementConfig.RunningSpeedModifier = 0f;
        }

        base.Remove();
    }
}
