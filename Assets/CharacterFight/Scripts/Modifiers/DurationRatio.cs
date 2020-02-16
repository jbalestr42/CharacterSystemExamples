using UnityEngine;

public class DurationRatio : Duration {

    public override float GetFactor() {
        return Param.inverse ? 1f - GetRatio() : GetRatio();
    }
}