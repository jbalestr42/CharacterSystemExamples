using UnityEngine;

public class SingleValueModifier : AAttributeModifier<SingleValueModifier.Params, Attribute<float>>
{
    [System.Serializable]
    public class Params : AttributeParam<float>
    {
        public int sourceAttributeType;
        public bool inverse;

        public Params()
            :this(0, 0, 0, false) { }

        public Params(int p_targetAttributeType, int p_targetAttributeValueType, int p_sourceAttributeType, bool p_inverse) 
            :base(0f, p_targetAttributeType, p_targetAttributeValueType)
        {
            sourceAttributeType = p_sourceAttributeType;
            inverse = p_inverse;
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
            float value = Param.inverse ? -_source.Value : _source.Value;
            p_attribute.SetValue(Param.attributeValueType, p_attribute.GetValue(Param.attributeValueType) + value);
            _isDone = true;
        }
    }

    public override bool IsOver()
    {
        return _isDone;
    }
}