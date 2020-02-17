using UnityEngine;

public class DurationRatioModifier : DurationModifier
{
    public override float GetFactor()
    {
        return Param.inverse ? 1f - GetRatio() : GetRatio();
    }
}