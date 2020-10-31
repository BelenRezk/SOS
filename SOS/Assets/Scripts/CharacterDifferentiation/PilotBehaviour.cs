using UnityEngine;

public class PilotBehaviour : CharacterDifferentiationBase
{
    public PilotBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 15f;
        movement.abilityDuration = 2f;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Pilot Ability");
    }

    public override void FinishSpecialAbility()
    {
        Debug.Log("Finish Pilot Ability");
    }
}