using UnityEngine;

/// <summary>
/// This class modify a resource based on the given param
/// Update the maximum value of the resource
/// Apply the regen value to the resource
/// </summary>
public class ResourceModifier : AAttributeModifier<ResourceModifier.Params, ResourceModifier.Attribute>
{
    [System.Serializable]
    public class Params : AttributeParam<float>
    {
        public int regenAttributeType;
        public int maxAttributeType;

        public Params()
            :this(0, 0, 0) { }

        public Params(int p_regenAttributeType, int p_maxAttributeType, int p_attributeType) 
            :base(0f, p_attributeType, 0)
        {
            regenAttributeType = p_regenAttributeType;
            maxAttributeType = p_maxAttributeType;
        }
    }

    /// <summary>
    /// This class manage a resource attribute such as Life or Mana
    /// </summary>
    public class Attribute : Attribute<float>
    {
        public Attribute(float p_value, float p_min, float p_max)
            : base(p_value)
        {
            SetValue(AttributeValueType.Add, 0f);
            SetValue(AttributeValueType.Min, p_min);
            SetValue(AttributeValueType.Max, p_max);
        }

        public override void AfterModifierUpdate()
        {
            float newBase = GetValue(AttributeValueType.Base) + GetValue(AttributeValueType.Add);
            SetValue(AttributeValueType.Base, Mathf.Clamp(newBase, GetValue(AttributeValueType.Min), GetValue(AttributeValueType.Max)));
            SetValue(AttributeValueType.Add, 0f);
        }

        public override float Value
        {
            get { return GetValue(AttributeValueType.Base); }
        }
    }

    Attribute<float> _regen;
    Attribute<float> _max;
    float _regenRate = 1f;
    float _timer = 0f;

    public override void OnStart(GameObject p_owner)
    {
        _regen = p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.regenAttributeType);
        _max = p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.maxAttributeType);
    }

    public override void ApplyModifierCast(GameObject p_owner, Attribute p_attribute)
    {
        _timer += Time.deltaTime;

        if (_timer >= _regenRate)
        {
            _timer -= _regenRate;
            Param.value = _regen.Value;
            p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.attributeType).SetValue(AttributeValueType.Add, _regen.Value);
            p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.attributeType).SetValue(AttributeValueType.Max, _max.Value);
        }
    }
}