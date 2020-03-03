using System;
using UnityEngine;

public class SelectCharacterInteraction : AInteraction
{
    IHasTarget _hasTarget = null;

    public SelectCharacterInteraction(IHasTarget p_hasTarget)
    {
        _hasTarget = p_hasTarget;
    }

    public override int GetLayer()
    {
        return Layers.UI;
    }

    public override bool IsValidTarget(GameObject p_target)
    {
        // Can be improved
        return p_target.GetComponent<BaseCharacter>() != null;
    }

    public override void OnMouseClick(GameObject p_target, Vector3 p_position)
    {
        Debug.Log("onclick");
        _hasTarget.SetTarget(p_target);
        InteractionManager.Instance.EndInteraction();
    }

    public override void OnMouseOver(GameObject p_target, Vector3 p_position)
    {
        Debug.Log("over");
    }

    public override void Cancel()
    {
        Debug.Log("cancel");
    }

    public override void End()
    {
        Debug.Log("end");
    }
}