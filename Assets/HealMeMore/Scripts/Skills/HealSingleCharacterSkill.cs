using System.Collections.Generic;
using UnityEngine;

public class HealSingleCharacterSkill : ASkill
{
    public float _manaCost;

	void Start()
    {
        List<IRequirement> requirements = new List<IRequirement>();
		requirements.Add(new GameStartReq());
        // todo add mana req

        AttributeManager attributeManager = GetComponent<AttributeManager>();
        IProgressTracker progressTracker = GetComponent<IProgressTrackerProvider>()?.CreateTracker();

		Init(0f, 5f, requirements, progressTracker);
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        AttributeManager attributeManager = target?.GetComponent<AttributeManager>();
        if (target != null && attributeManager != null)
        {
            attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.SingleValue, target, new SingleValueModifier.AttParams(AttributeType.Health, AttributeValueType.Add, false, p_owner, AttributeType.HealPower)));
            GetComponent<AttributeManager>()?.AddModifier(Factory.GetModifier(AttributModifierType.SingleValue, p_owner, new SingleValueModifier.RawParams(AttributeType.Mana, AttributeValueType.Add, true, _manaCost)));
        }
	}
}
