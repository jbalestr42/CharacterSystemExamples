using System.Collections.Generic;
using UnityEngine;

public class HitCharacterSkill : AAttributeSkill
{
    AttributeManager _attributeManager = null;

	void Start()
    {
        List<IRequirement> requirements = new List<IRequirement>();
		requirements.Add(new GameStartReq());

        _attributeManager = GetComponent<AttributeManager>();
        IProgressTracker progressTracker = GetComponent<IProgressTrackerProvider>()?.CreateTracker();

		Init(null, _attributeManager.GetAttribute<float>(AttributeType.AttackRate), requirements, progressTracker);
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        if (target != null)
        {
            _attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.SimpleValue, p_owner, new SingleValueModifier.Params(AttributeType.Health, AttributeValueType.Add, AttributeType.Damage, true)));
        }
	}
}
