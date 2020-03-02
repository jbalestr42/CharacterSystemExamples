using UnityEngine;

[RequireComponent(typeof(AttributeManager))]
public class Player : MonoBehaviour, IHasTarget
{
    GameObject _target = null;

	void Awake()
    {
		AttributeManager attributeManager = GetComponent<AttributeManager>();
        attributeManager.AddAttribute(AttributeType.Mana, new ResourceModifier.Attribute(100, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaMax, new BasicAttribute(150, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaRegen, new BasicAttribute(0.5f, 0, 100));
		attributeManager.AddAttribute(AttributeType.HealPower, new BasicAttribute(50, 0, 1000));
        attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, gameObject, new ResourceModifier.Params(AttributeType.ManaRegen, AttributeType.ManaMax, AttributeType.Mana)));

        SkillGroup progressTrackerProvider = GetComponent<SkillGroup>();
        GameObject progressTracker = progressTrackerProvider.CreateSkill(DataManager.Instance.CreateDisplaySkillData(SkillType.HealSingleCharacter));
        ASkillController skillController = progressTracker.GetComponent<ASkillController>();
        ASkill skill = new HealSingleCharacterSkill(gameObject, 3f, 50f);
        skillController.Skill = new InteractionSkill(new SelectCharacterInteraction(this), skill);
        skillController.ProgressTracker = progressTracker.GetComponent<IProgressTracker>();
	}

    public GameObject GetTarget()
    {
        return _target;
    }

    public void SetTarget(GameObject p_target)
    {
        _target = p_target;
    }
}
