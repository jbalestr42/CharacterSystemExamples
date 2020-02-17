using UnityEngine;

public class GameStartReq : IRequirement
{

	public bool IsValid(GameObject p_owner)
    {
        return Game.Instance.CanFight;
	}
}