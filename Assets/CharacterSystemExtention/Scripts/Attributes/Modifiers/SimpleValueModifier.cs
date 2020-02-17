using UnityEngine;

public class SingeValueModifier : AAttributeModifier<SingeValueModifier.Params, Attribute<float>>
{
    [System.Serializable]
    public class Params : AttributeParam<float>
    {
        public int sourceAttributeType;

        public Params()
            :this(0, 0, 0) { }

        public Params(int p_targetAttributeType, int p_targetAttributeValueType, int p_sourceAttributeType) 
            :base(0f, p_targetAttributeType, p_targetAttributeValueType)
        {
            sourceAttributeType = p_sourceAttributeType;
        }
    }

    Attribute<float> _source;
    
    bool _isDone = false;

    public override void OnStart(GameObject p_owner)
    {
        _source = p_owner.GetComponent<AttributeManager>().GetAttribute<float>(Param.sourceAttributeType);
    }

    public override void ApplyModifierCast(GameObject p_owner, Attribute<float> p_attribute)
    {
        if (!_isDone)
        {
            p_attribute.SetValue(Param.attributeValueType, p_attribute.GetValue(Param.attributeValueType) - _source.Value);
            _isDone = true;
        }
    }

    public override bool IsOver()
    {
        return _isDone;
    }
}