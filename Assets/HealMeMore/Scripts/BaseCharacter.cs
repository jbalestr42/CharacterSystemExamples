using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AttributeManager))]
public class BaseCharacter : MonoBehaviour
{
	void Start()
    {
		AttributeManager attributeManager = GetComponent<AttributeManager>();
        attributeManager.AddAttribute(AttributeType.Health, new ResourceAttribute(100, 0, 1000));
		attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(1, 0, 100));
		attributeManager.AddAttribute(AttributeType.Speed, new BasicAttribute(5, 0, 10));
		attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(80, 0, 1000));
        attributeManager.AddAttribute(AttributeType.CanUseSkill, new Attribute<bool>(true));
        attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, gameObject, new ResourceAttributeParam(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
	}
}
