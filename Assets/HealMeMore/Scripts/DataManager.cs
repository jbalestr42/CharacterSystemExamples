using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fastest way to retrieve data for now
/// </summary>
public class DataManager : Singleton<DataManager>
{
    public SkillGroup.DisplayData HitSingleCharacterDisplayData = null;
    public SkillGroup.DisplayData HealSingleCharacterDisplayData = null;

    public SkillGroup.DisplayData CreateDisplaySkillData(SkillType p_skillType)
    {
        SkillGroup.DisplayData displayData = null;
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
}
