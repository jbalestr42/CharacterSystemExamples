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
        BaseCharacter character = _uiManager.AddLeftTeam();
        Factory.InitCharacter(CharacterType.Warrior, character);
        LeftTeam.Add(character);

        character = _uiManager.AddRightTeam();
        Factory.InitCharacter(CharacterType.Goblin, character);
        RightTeam.Add(character);
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
