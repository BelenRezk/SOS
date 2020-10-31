using UnityEngine;

public class HippieBehaviour : CharacterDifferentiationBase
{
    public HippieBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 40f;
        movement.abilityDuration = 1f;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Hippie Ability");
    }

    public override void FinishSpecialAbility()
    {
        Debug.Log("Finish Hippie Ability");
    }
}