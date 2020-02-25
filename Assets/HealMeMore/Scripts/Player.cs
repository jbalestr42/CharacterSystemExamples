using UnityEngine;

[RequireComponent(typeof(AttributeManager))]
public class Player : MonoBehaviour, IHasTarget
{
	void Awake()
    {
		AttributeManager attributeManager = GetComponent<AttributeManager>();
        attributeManager.AddAttribute(AttributeType.Mana, new ResourceModifier.Attribute(100, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaMax, new BasicAttribute(150, 0, 1000));
		attributeManager.AddAttribute(AttributeType.ManaRegen, new BasicAttribute(0.5f, 0, 100));
		attributeManager.AddAttribute(AttributeType.HealPower, new BasicAttribute(50, 0, 1000));
        attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, gameObject, new ResourceModifier.Params(AttributeType.ManaRegen, AttributeType.ManaMax, AttributeType.Mana)));

        IProgressTrackerProvider progressTrackerProvider = GetComponent<IProgressTrackerProvider>();

        // TODO Init all this from data
         // TODO: rename ?
        GameObject progressTracker = progressTrackerProvider?.CreateTracker();
        ASkillController skillController = progressTracker.GetComponent<ASkillController>();
        skillController.Skill = new HealSingleCharacterSkill(gameObject, 0f, 3f, 50f);
        skillController.ProgressTracker = progressTracker.GetComponent<IProgressTracker>();
	}

    public GameObject GetTarget()
    {
        return Game.Instance.LeftTeam[0].gameObject;
    }
}
