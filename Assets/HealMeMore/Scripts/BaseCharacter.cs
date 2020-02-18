using UnityEngine;
using System.Collections.Generic;

public enum Teams
{
    Left,
    Right
}

[RequireComponent(typeof(AttributeManager))]
public class BaseCharacter : MonoBehaviour, IHasTarget
{
    public Teams Team { get; set; }

	void Start()
    {
        // TODO: init from SO
		AttributeManager attributeManager = GetComponent<AttributeManager>();
        attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(100, 0, 1000));
		attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(250, 0, 1000));
		attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(1, 0, 100));
		attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(80, 0, 1000));
        attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
	}

    public GameObject GetTarget()
    {
        List<BaseCharacter> characters = Game.Instance.GetOppositeTeam(Team);
        return characters.Count > 0 ? characters[0].gameObject : null;
    }
}
