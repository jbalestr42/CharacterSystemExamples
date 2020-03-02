using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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
                Debug.Log("The enum " + p_modifierType.ToString() + " is not recognized.");
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
            break;
        }

        return skill;
    }
}
