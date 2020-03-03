using UnityEngine;
using System.Collections.Generic;

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
        return characters.Count > 0 ? characters[Random.Range(0, characters.Count)].gameObject : null;
    }

    public void SetTarget(GameObject p_target)
    {
        //TODO
    }
}
