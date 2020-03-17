using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AttributeManager))]
public class BaseCharacter : MonoBehaviour, IHasTarget
{
    [SerializeField]
    CharacterType _characterType = CharacterType.None;
    
    public Teams Team { get; set; }

    GameObject _currentTarget = null;

	void Awake()
    {
        DataManager.Instance.InitCharacter(_characterType, this);
	}

    public GameObject GetTarget()
    {
        GameObject target = _currentTarget;

        if (target == null)
        {
            List<BaseCharacter> characters = Game.Instance.GetOppositeTeam(Team);
            target = characters.Count > 0 ? characters[Random.Range(0, characters.Count)].gameObject : null;

        }
        return target;
    }

    public void SetTarget(GameObject p_target)
    {
        _currentTarget = p_target;
    }
}
