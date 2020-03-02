using UnityEngine;

/// <summary>
/// Fastest way to retrieve data for now
/// </summary>
public class DataManager : Singleton<DataManager>
{
    public SkillIcon.DisplayData HitSingleCharacterDisplayData = null;
    public SkillIcon.DisplayData HealSingleCharacterDisplayData = null;

    public SkillIcon.DisplayData CreateDisplaySkillData(SkillType p_skillType)
    {
        SkillIcon.DisplayData displayData = null;
        switch (p_skillType)
        {
            case SkillType.HealSingleCharacter:
                displayData = HealSingleCharacterDisplayData;
                break;
                
            case SkillType.HitSingleCharacter:
                displayData = HitSingleCharacterDisplayData;
                break;

            default:
                break;
        }

        return displayData;
    }
    
    public void InitCharacter(CharacterType p_charachterType, BaseCharacter p_character)
    {
        AttributeManager attributeManager = p_character.GetComponent<AttributeManager>();
        SkillGroup progressTrackerProvider = p_character.GetComponent<SkillGroup>();

        switch (p_charachterType)
        {
            case CharacterType.None:
                // Nothing to do
            break;

            case CharacterType.Warrior:
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(100, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(250, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(1, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(5, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
                
                GameObject skill = progressTrackerProvider.CreateSkill(DataManager.Instance.CreateDisplaySkillData(SkillType.HitSingleCharacter));
                ASkillController skillController = skill.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = skill.GetComponent<IProgressTracker>();
            break;

            case CharacterType.Goblin:
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(100, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(250, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(1, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(5, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
            break;

            default:
                Debug.Log("The type " + p_charachterType.ToString() + " is not implemented.");
            break;
        }
    }
}
