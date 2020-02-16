using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DurationAttributeParam<T> : AttributeParam<T> {
    public float duration;
    public bool inverse;
    public IProgressTracker progressTracker;

    public DurationAttributeParam()
        :this(null, false, 0f, default(T), 0, 0) { }

    public DurationAttributeParam(IProgressTracker p_progressTracker, bool p_inverse, float p_duration, T p_value, int p_attributeType, int p_attributeValueType)
        :base(p_value, p_attributeType, p_attributeValueType) {
        progressTracker = p_progressTracker;
        inverse = p_inverse;
        duration = p_duration;
    }
}
