using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
    [SerializeField]
    UIManager _uiManager = null;

    public List<BaseCharacter> LeftTeam { get; private set; }
    public List<BaseCharacter> RightTeam { get; private set; }
    public bool CanFight { get; set; }

    void Start()
    {
        LeftTeam = new List<BaseCharacter>();
        RightTeam = new List<BaseCharacter>();

        CanFight = false;

        Init();
    }

    void Init()
    {
        BaseCharacter c = _uiManager.AddLeftTeam();
        
        ProgressTrackerGroupUI progressTrackerProvider = c.GetComponent<ProgressTrackerGroupUI>();
        GameObject progressTracker = progressTrackerProvider.CreateTracker(DataManager.Instance.CreateTrackerData(SkillType.HitSingleCharacter));
        ASkillController skillController = progressTracker.GetComponent<ASkillController>();
        skillController.Skill = new HitSingleCharacterSkill(c.gameObject, AttributeType.AttackRate);
        skillController.ProgressTracker = progressTracker.GetComponent<IProgressTracker>();

        LeftTeam.Add(c);
        RightTeam.Add(_uiManager.AddRightTeam());
    }

    public void OnStartClick()
    {
        CanFight = !CanFight;
    }

    public List<BaseCharacter> GetOppositeTeam(Teams team)
    {
        if (team == Teams.Left)
        {
            return RightTeam;
        }
        else
        {
            return LeftTeam;
        }
    }
}
