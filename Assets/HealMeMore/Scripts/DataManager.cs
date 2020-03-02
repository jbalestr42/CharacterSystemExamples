using System;
using System.Collections.Generic;
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

    public List<BaseCharacter> InitCharacterFromLevel(int p_level, Func<BaseCharacter> p_characterCreator)
    {
        List<KeyValuePair<CharacterType, int>> types = new List<KeyValuePair<CharacterType, int>>();

        switch (p_level)
        {
            case 0:
                types.Add(new KeyValuePair<CharacterType, int>(CharacterType.Warrior, 1));
                types.Add(new KeyValuePair<CharacterType, int>(CharacterType.LittleWarrior, 3));
                break;

            case 1:
                types.Add(new KeyValuePair<CharacterType, int>(CharacterType.Goblin, 2));
                types.Add(new KeyValuePair<CharacterType, int>(CharacterType.Spider, 5));
                break;

            default:
                Debug.Log("The level " + p_level + " is not implemented.");
                break;
        }

        int characterIndex = UnityEngine.Random.Range(0, types.Count);
        Debug.Log("Creating " + types[characterIndex].Value + " " + types[characterIndex].Key + ".");
        
        List<BaseCharacter> characters = new List<BaseCharacter>();
        for (int i = 0; i < types[characterIndex].Value; i++)
        {
            BaseCharacter character = p_characterCreator();
            InitCharacter(types[characterIndex].Key, character);
            characters.Add(character);
        }
        
        return characters;
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

            case CharacterType.LittleWarrior:
            {
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(35, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(35, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(0, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(1, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(1, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
                
                GameObject skill = progressTrackerProvider.CreateSkill(CreateDisplaySkillData(SkillType.HitSingleCharacter));
                ASkillController skillController = skill.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = skill.GetComponent<IProgressTracker>();
            }
            break;

            case CharacterType.Warrior:
            {
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(120, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(120, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(0, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(5, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
                
                GameObject skill = progressTrackerProvider.CreateSkill(CreateDisplaySkillData(SkillType.HitSingleCharacter));
                ASkillController skillController = skill.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = skill.GetComponent<IProgressTracker>();
            }
            break;

            case CharacterType.Goblin:
            {
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(80, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(80, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(0.5f, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(5, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
            
                GameObject skill = progressTrackerProvider.CreateSkill(CreateDisplaySkillData(SkillType.HitSingleCharacter));
                ASkillController skillController = skill.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = skill.GetComponent<IProgressTracker>();
            }
            break;

            case CharacterType.Spider:
            {
                attributeManager.AddAttribute(AttributeType.Health, new ResourceModifier.Attribute(20, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthMax, new BasicAttribute(20, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.HealthRegen, new BasicAttribute(0.5f, 0, 100));
		        attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(1, 0, 1000));
		        attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(2, 0, 1000));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.Resource, p_character.gameObject, new ResourceModifier.Params(AttributeType.HealthRegen, AttributeType.HealthMax, AttributeType.Health)));
                attributeManager.AddModifier(Factory.GetModifier(AttributModifierType.DurationRatio, p_character.gameObject, new DurationModifier.Params<float>(null, false, 10, 2.0f, AttributeType.AttackRate, AttributeValueType.RelativeBonus)));
            
                
                GameObject skill = progressTrackerProvider.CreateSkill(CreateDisplaySkillData(SkillType.HitSingleCharacter));
                ASkillController skillController = skill.GetComponent<ASkillController>();
                skillController.Skill = new HitSingleCharacterSkill(p_character.gameObject, AttributeType.AttackRate);
                skillController.ProgressTracker = skill.GetComponent<IProgressTracker>();
            }
            break;

            default:
                Debug.Log("The type " + p_charachterType.ToString() + " is not implemented.");
            break;
        }
    }
}
