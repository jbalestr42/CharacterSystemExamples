using UnityEngine;
using System.Collections.Generic;

public enum Teams
{
    Left,
    Right
}

[RequireComponent(typeof(AttributeManager))]
public class BaseCharacter : MonoBehaviour, IHasTarget
{
    [SerializeField]
    CharacterType _characterType = CharacterType.None;
    
    public Teams Team { get; set; }

	void Awake()
    {
        DataManager.Instance.InitCharacter(_characterType, this);
	}

    public GameObject GetTarget()
    {
        List<BaseCharacter> characters = Game.Instance.GetOppositeTeam(Team);
        return characters.Count > 0 ? characters[0].gameObject : null;
    }

    public void SetTarget(GameObject p_target)
    {
        //TODO
    }
}
