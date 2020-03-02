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
}
