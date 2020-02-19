using UnityEngine;

public class CharacterInteraction : AInteraction
{
    public CharacterInteraction()
    {
    }

    public override int GetLayer()
    {
        return Layers.UI;
    }

    public override void OnMouseClick(Vector3 position)
    {
        InteractionManager.Instance.EndInteraction();
    }

    public override void OnMouseOver(Vector3 position)
    {
    }

    public override void Cancel()
    {
    }

    public override void End()
    {
    }
}