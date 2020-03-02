using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This factory can be improved by creating data instead of hardcoded values
/// </summary>
public static class Factory
{
    public static IAttributeModifier GetModifier(AttributModifierType p_modifierType, GameObject p_owner, BaseAttributeParam p_param)
    {
        IAttributeModifier modifierFactor = null;
        switch (p_modifierType)
        {
            case AttributModifierType.Resource:
                modifierFactor = new ResourceModifier();
                break;

            case AttributModifierType.DurationRatio:
                modifierFactor = new DurationRatioModifier();
                break;


            case AttributModifierType.Duration:
                modifierFactor = new DurationModifier();
                break;


            case AttributModifierType.SingleValue:
                modifierFactor = new SingleValueModifier();
                break;

            default:
                Debug.Log("The type " + p_modifierType.ToString() + " is not implemented.");
                return null;
        }

        modifierFactor.SetAttributeParam(p_param);
        modifierFactor.OnStart(p_owner);

        return modifierFactor;
    }

    public static ASkill GetSkill(SkillType p_skillType, params object[] p_parameters)
    {
        ASkill skill = null;

        switch (p_skillType)
        {
            case SkillType.HitSingleCharacter:
                Assert.IsTrue(p_parameters.Length == 2);

                skill = new HitSingleCharacterSkill((GameObject)p_parameters[0], (int)p_parameters[1]);
            break;

            case SkillType.HealSingleCharacter:
                Assert.IsTrue(p_parameters.Length == 3);

                skill = new HealSingleCharacterSkill((GameObject)p_parameters[0], (float)p_parameters[1], (float)p_parameters[2]);
            break;

            default:
                Debug.Log("The type " + p_skillType.ToString() + " is not implemented.");
            break;
        }

        return skill;
    }
    
    public static void InitCharacter(CharacterType p_charachterType, BaseCharacter p_character)
    {
        AttributeManager attributeManager = p_character.GetComponent<AttributeManager>();
        ProgressTrackerGroupUI progressTrackerProvider = p_character.GetComponent<ProgressTrackerGroupUI>();

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
                attributeManager.AddModifier(GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
                
                GameObject progressTracker = progressTrackerProvider.CreateTracker(DataManager.Instance.CreateTrackerData(SkillType.HitSingleCharacter));
                ASkillController skillController = progressTracker.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = progressTracker.GetComponent<IProgressTracker>();
            break;

            case CharacterType.Goblin:
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(100, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(250, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(1, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(5, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
            break;

            default:
                Debug.Log("The type " + p_charachterType.ToString() + " is not implemented.");
            break;
        }
    }
}
