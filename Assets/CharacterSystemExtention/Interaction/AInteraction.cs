using System;
using UnityEngine;

public abstract class AInteraction
{
    public Action OnInteractionDone { get; set; } = null;
    public Action OnInteractionCancelled { get; set; } = null;

    public abstract bool IsValidTarget(GameObject p_target);
    public abstract int GetLayer();
    public virtual void OnMouseClick(GameObject p_target, Vector3 p_position) { }
    public virtual void OnMouseEnter(GameObject p_target, Vector3 p_position) { }
    public virtual void OnMouseOver(GameObject p_target, Vector3 p_position) { }
    public virtual void OnMouseExit() { }
    public virtual void Cancel() { }
    public virtual void End() { }

    // bool ShouldCancelInteraction()
}