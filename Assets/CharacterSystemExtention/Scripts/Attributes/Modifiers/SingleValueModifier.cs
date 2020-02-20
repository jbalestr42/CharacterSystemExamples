using UnityEngine;

public class SingleValueModifier : AAttributeModifier<SingleValueModifier.BaseParams, Attribute<float>>
{
    [System.Serializable]
    public abstract class BaseParams : AttributeParam<float>
    {
        public int sourceAttributeType;
        public bool inverse;

        public BaseParams(int p_ownerAttributeType, int p_ownerAttributeValueType, bool p_inverse) 
            :base(0f, p_ownerAttributeType, p_ownerAttributeValueType)
        {
            inverse = p_inverse;
        }

        public abstract float Value { get; }
    }
    
    [System.Serializable]
    public class AttParams : BaseParams
    {
        Attribute<float> _source;
        
        public AttParams(int p_ownerAttributeType, int p_ownerAttributeValueType, bool p_inverse, GameObject p_sourceGameObject, int p_sourceAttributeType) 
            :base(p_ownerAttributeType, p_ownerAttributeValueType, p_inverse)
        {
            _source = p_sourceGameObject.GetComponent<AttributeManager>().GetAttribute<float>(p_sourceAttributeType);
        }

        public override float Value
        {
            get { return _source.Value; }
        }
    }

    [System.Serializable]
    public class RawParams : BaseParams
    {
        float _value = 0f;

        public RawParams(int p_ownerAttributeType, int p_ownerAttributeValueType, bool p_inverse, float p_value) 
            :base(p_ownerAttributeType, p_ownerAttributeValueType, p_inverse)
        {
            _value = p_value;
        }

        public override float Value
        {
            get { return _value; }
        }
    }

    bool _isDone = false;

    public override void ApplyModifierCast(GameObject p_owner, Attribute<float> p_attribute)
    {
        if (!_isDone)
        {
            float value = Param.inverse ? -Param.Value : Param.Value;
            p_attribute.SetValue(Param.attributeValueType, p_attribute.GetValue(Param.attributeValueType) + value);
            _isDone = true;
        }
    }

    public override bool IsOver()
    {
        return _isDone;
    }
}
