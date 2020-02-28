using UnityEngine;

public interface IHasTarget // TODO rename 
{
    GameObject GetTarget();
    void SetTarget(GameObject p_target);
}
