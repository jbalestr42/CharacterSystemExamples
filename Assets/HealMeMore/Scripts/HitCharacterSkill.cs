using System.Collections.Generic;
using UnityEngine;

public class HitCharacterSkill : ASkill
{
	void Start()
    {
        List<IRequirement> requirements = new List<IRequirement>();
		requirements.Add(new GameStartReq());
		base.Init(0f, 5.0f, requirements, null);
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        if (target != null)
        {
            AttributeManager attManager = p_owner.GetComponent<AttributeManager>();
            attManager.AddModifier(Factory.GetModifier(AttributModifierType.SimpleValue, p_owner, new SingeValueModifier.Params(AttributeType.Health, AttributeValueType.Add, AttributeType.Damage)));
        }
	}
}
