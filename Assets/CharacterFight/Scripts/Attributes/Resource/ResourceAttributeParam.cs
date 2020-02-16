using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class ResourceAttributeParam : AttributeParam<float> {
    public int regenAttributeType;
    public int maxAttributeType;

    public ResourceAttributeParam()
        :this(0, 0, 0) { }

    public ResourceAttributeParam(int p_regenAttributeType, int p_maxAttributeType, int p_attributeType) 
        :base(0f, p_attributeType, 0) {
        regenAttributeType = p_regenAttributeType;
        maxAttributeType = p_maxAttributeType;
    }
}