using UnityEngine;

public class DurationModifier : AAttributeModifier<DurationModifier.Params<float>, Attribute<float>>
{
    [System.Serializable]
    public class Params<T> : AttributeParam<T>
    {
        public float duration;
        public bool inverse;
        public IProgressTracker progressTracker;

        public Params()
            : this(null, false, 0f, default(T), 0, 0) { }

        public Params(IProgressTracker p_progressTracker, bool p_inverse, float p_duration, T p_value, int p_attributeType, int p_attributeValueType)
            : base(p_value, p_attributeType, p_attributeValueType)
        {
            progressTracker = p_progressTracker;
            inverse = p_inverse;
            duration = p_duration;
        }
    }

    protected float _endOfEffect;

    public override void OnStart(GameObject p_owner)
    {
        _endOfEffect = Time.realtimeSinceStartup + Param.duration;
    }

    public override void ApplyModifierCast(GameObject p_owner, Attribute<float> p_attribute)
    {
        if (Param.progressTracker != null)
        {
            Param.progressTracker.UpdateProgress(GetRatio(), _endOfEffect - Time.realtimeSinceStartup);
        }
        // TODO this computation "p_attribute.GetValue(Param.attributeValueType) + Param.value" as lambda ? override ?
        // How to manage the way we modify the data ?
        // How to manage other type ? bool ?
        p_attribute.SetValue(Param.attributeValueType, p_attribute.GetValue(Param.attributeValueType) + Param.value * GetFactor());
    }

    public override void OnEnd(GameObject p_owner)
    {
        if (Param.progressTracker != null)
        {
            Param.progressTracker.OnEnd();
        }
    }

    public override bool IsOver()
    {
        return (_endOfEffect - Time.realtimeSinceStartup) <= 0.0f;
    }

    public virtual float GetFactor()
    {
        return 1f;
    }

    public float GetRatio()
    {
        return Mathf.Clamp((_endOfEffect - Time.realtimeSinceStartup) / Param.duration, 0.0f, 1.0f);
    }
}