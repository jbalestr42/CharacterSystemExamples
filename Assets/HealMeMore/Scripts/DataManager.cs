using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fastest way to retrieve data for now
/// </summary>
public class DataManager : Singleton<DataManager>
{
    public ProgressTrackerGroupUI.TrackerData HitSingleCharacterTrackerData = null;
    public ProgressTrackerGroupUI.TrackerData HealSingleCharacterTrackerData = null;

    public ProgressTrackerGroupUI.TrackerData CreateTrackerData(SkillType p_skillType)
    {
        ProgressTrackerGroupUI.TrackerData trackerdata = null;
        switch (p_skillType)
        {
            case SkillType.HealSingleCharacter:
                trackerdata = HealSingleCharacterTrackerData;
                break;
                
            case SkillType.HitSingleCharacter:
                trackerdata = HitSingleCharacterTrackerData;
                break;

            default:
                break;
        }

        return trackerdata;
    }
}
