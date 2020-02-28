using UnityEngine;
using UnityEngine.Assertions;

public class HealSingleCharacterSkill : ASkill
{
    public float _manaCost;

	public HealSingleCharacterSkill(GameObject p_owner, float p_cooldownDuration, float manaCost)
        :base(p_owner, p_cooldownDuration)
    {
        _manaCost = manaCost;
	
        Requirements.Add(new GameStartReq());
        //Requirements.Add(new HasTargetReq()); //TODO same for hitsinglecharacter
		Requirements.Add(new AttributeComparisonReq(AttributeType.Mana, _manaCost, AttributeComparisonReq.ComparisonType.GreaterThanOrEqual));
    }

    public override void Cast(GameObject p_owner)
    {
        GameObject target = p_owner.GetComponent<IHasTarget>()?.GetTarget();
        AttributeManager attributeManager = target.GetComponent<AttributeManager>();
        if (attributeManager != null)
        {
            attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.SingleValue, target, new SingleValueModifier.AttParams(AttributeType.Health, AttributeValueType.Add, false, Owner, AttributeType.HealPower)));
            Owner.GetComponent<AttributeManager>()?.AddModifier(Factory.GetModifier(AttributModifierType.SingleValue, Owner, new SingleValueModifier.RawParams(AttributeType.Mana, AttributeValueType.Add, true, _manaCost)));
        }
	}
}