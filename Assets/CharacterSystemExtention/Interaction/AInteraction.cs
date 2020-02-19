using UnityEngine;

public abstract class AInteraction
{
    public abstract int GetLayer();
    public virtual void OnMouseClick(Vector3 position) { }
    public virtual void OnMouseEnter(Vector3 position) { }
    public virtual void OnMouseOver(Vector3 position) { }
    public virtual void OnMouseExit() { }
    public virtual void Cancel() { }
    public virtual void End() { }
}