using UnityEngine;

[RequireComponent(typeof(AttributeManager))]
public class Player : MonoBehaviour
{
	void Start()
    {
		AttributeManager attributeManager = GetComponent<AttributeManager>();
        attributeManager.AddAttribute(AttributeType.Mana, new ResourceModifier.Attribute(100, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaMax, new BasicAttribute(150, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaRegen, new BasicAttribute(0.5f, 0, 100));
		attributeManager.AddAttribute(AttributeType.HealPower, new BasicAttribute(50, 0, 1000));
        attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, gameObject, new ResourceModifier.Params(AttributeType.ManaRegen, AttributeType.ManaMax, AttributeType.Mana)));
	}
}
