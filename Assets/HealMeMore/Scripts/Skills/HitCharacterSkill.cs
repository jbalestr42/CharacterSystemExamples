using System.Collections.Generic;
using UnityEngine;

public class HitCharacterSkill : AAttributeSkill
{
	void Start()
    {
        List<IRequirement> requirements = new List<IRequirement>();
		requirements.Add(new GameStartReq());

        AttributeManager attributeManager = GetComponent<AttributeManager>();
        IProgressTracker progressTracker = GetComponent<IProgressTrackerProvider>()?.CreateTracker();

		Init(null, attributeManager.GetAttribute<float>(AttributeType.AttackRate), requirements, progressTracker);
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        AttributeManager attributeManager = target?.GetComponent<AttributeManager>();
        if (target != null && attributeManager != null)
        {
            attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.SimpleValue, target, new SingleValueAttributeModifier.Params(AttributeType.Health, AttributeValueType.Add, p_owner, AttributeType.Damage, true)));
        }
	}
}
