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
        List<BaseCharacter> characters = DataManager.Instance.InitCharacterFromLevel(0, () => { return _uiManager.AddLeftTeam(); });
        foreach (BaseCharacter character in characters)
        {
            LeftTeam.Add(character);
        }
        
        characters = DataManager.Instance.InitCharacterFromLevel(1, () => { return _uiManager.AddRightTeam(); });
        foreach (BaseCharacter character in characters)
        {
            RightTeam.Add(character);
        }
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
