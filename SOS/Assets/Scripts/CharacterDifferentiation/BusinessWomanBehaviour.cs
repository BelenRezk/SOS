using UnityEngine;

public class BusinessWomanBehaviour : CharacterDifferentiationBase
{
    public BusinessWomanBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 35f;
        movement.abilityDuration = 1f;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Business Woman Ability");
    }

    public override void FinishSpecialAbility()
    {
        Debug.Log("Finish Business Woman Ability");
    }
}