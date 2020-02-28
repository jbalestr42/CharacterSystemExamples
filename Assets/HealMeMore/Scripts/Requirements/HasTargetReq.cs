using UnityEngine;
using UnityEngine.Assertions;

public class HasTargetReq : IRequirement
{
    public bool IsValid(GameObject p_owner)
    {
        IHasTarget hasTarget = p_owner.GetComponent<IHasTarget>();

        Assert.IsNotNull(hasTarget, "This component must have implements IHasTarget.");

        return hasTarget?.GetTarget() != null;
    }
}