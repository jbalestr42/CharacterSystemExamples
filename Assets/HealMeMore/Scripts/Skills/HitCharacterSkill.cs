using UnityEngine;

public class HitSingleCharacterSkill : AAttributeSkill
{
	public HitSingleCharacterSkill(GameObject p_owner, int p_attributeCooldown)
        :base(p_owner, null, null)
    {
		Requirements.Add(new GameStartReq());
        
        AttributeManager attributeManager = p_owner.GetComponent<AttributeManager>();
        CooldownDurationAtt = attributeManager.GetAttribute<float>(p_attributeCooldown);
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        AttributeManager attributeManager = target?.GetComponent<AttributeManager>();
        if (target != null && attributeManager != null)
        {
            attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.SingleValue, target, new SingleValueModifier.AttParams(AttributeType.Health, AttributeValueType.Add, true, p_owner, AttributeType.Damage)));
        }
	}
}
