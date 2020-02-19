using UnityEngine;

public class SingleValueAttributeModifier : AAttributeModifier<SingleValueAttributeModifier.Params, Attribute<float>>
{
    [System.Serializable]
    public class Params : AttributeParam<float>
    {
        public int sourceAttributeType;
        public bool inverse;
        public GameObject sourceGameObject = null;

        public Params()
            :this(0, 0, null, 0, false) { }

        public Params(int p_ownerAttributeType, int p_ownerAttributeValueType, GameObject p_sourceGameObject, int p_sourceAttributeType, bool p_inverse) 
            :base(0f, p_ownerAttributeType, p_ownerAttributeValueType)
        {
            sourceGameObject = p_sourceGameObject;
            sourceAttributeType = p_sourceAttributeType;
            inverse = p_inverse;
        }
    }

    Attribute<float> _source;
    
    bool _isDone = false;

    public override void OnStart(GameObject p_owner)
    {
        _source = Param.sourceGameObject.GetComponent<AttributeManager>().GetAttribute<float>(Param.sourceAttributeType);
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