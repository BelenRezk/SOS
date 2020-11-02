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
        if (shouldPlayMusic)
        {
            audioManager.Stop("MainMusic");
            audioManager.Stop("BananaMusic");
            audioManager.Play("RadarBlip");
            audioManager.Play("TargetAcquired");
        }
    }

    public override void FinishSpecialAbility()
    {
        if (shouldPlayMusic)
        {
            audioManager.Stop("RadarBlip");
            if (movement.isUsingBanana)
                audioManager.Play("BananaMusic");
            else
                audioManager.Play("MainMusic");
        }
        Debug.Log("Finish Pilot Ability");
    }
}