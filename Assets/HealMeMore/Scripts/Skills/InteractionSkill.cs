using UnityEngine;

/// <summary>
/// This class trigger an interaction when the skill's requirements are met
/// Then the function OnInteractionDone is called by the interaction to cast the skill
/// </summary>
public class InteractionSkill : ASkill
{
    AInteraction _interaction = null;
    ASkill _skill = null;
    
    public override float CooldownDuration { get { return _skill == null ? 0f : _skill.CooldownDuration; } }

	public InteractionSkill(AInteraction p_interaction, ASkill p_skill)
        :base("[Interaction] " + p_skill.Name, p_skill?.Owner, 0f)
    {
        _interaction = p_interaction;
        _interaction.OnInteractionDone += OnInteractionDone;
        _interaction.OnInteractionCancelled += OnInteractionCancelled;
        _skill = p_skill;
    }

    public override void OnCast(GameObject p_owner)
    {
        Cast(p_owner);
        // Don't set IsDone to true here
	}

    protected override void Cast(GameObject p_owner)
    {
        Debug.Log("Cast interaction skill.");
        InteractionManager.Instance.SetInteraction(_interaction);
	}
    
    public virtual void OnInteractionDone()
    {
        // No sure if the test is needed
        Debug.Log("OnInteractionDone");
        if (_skill?.Owner?.GetComponent<IHasTarget>()?.GetTarget() != null)
        {
            Debug.Log("OnInteractionDone success");
            _skill.OnCast(Owner);
            // Terminate skill
        }
        else
        {
            Debug.Log("OnInteractionDone failed: no target");
            // Cancel skill
        }

        // Terminate the skill at this point
        IsDone = true;
    }
    
    public virtual void OnInteractionCancelled()
    {
        Debug.Log("OnInteractionCancelled");
    }
    
    public override bool AreRequirementsValidated()
    {
        return _skill != null ? _skill.AreRequirementsValidated() : false;
    }
}