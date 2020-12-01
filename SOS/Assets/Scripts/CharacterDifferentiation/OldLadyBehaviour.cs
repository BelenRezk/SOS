using UnityEngine;
using System;

public class OldLadyBehaviour : CharacterDifferentiationBase
{
    private float duration = 10f;
    private float speedMultiplier = 2f;
    public OldLadyBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 35f;
        movement.abilityDuration = duration;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Old Lady Ability");
        movement.speed *= speedMultiplier;
        movement.currentInvincibility += duration;
        if (shouldPlayMusic)
        {
            try{
                audioManager.Stop("MainMusic");
                audioManager.Stop("BananaMusic");
                audioManager.Play("OldLadyAbilityMusic");
            }
            catch(Exception){}
        }
    }

    public override void FinishSpecialAbility()
    {
        Debug.Log("Finish Old Lady Ability");
        movement.speed /= speedMultiplier;
        if (shouldPlayMusic)
        {
            try{
                audioManager.Stop("OldLadyAbilityMusic");
            }
            catch(Exception){}
            if (movement.isUsingBanana)
                try{
                    audioManager.Play("BananaMusic");
                }
                catch(Exception){}
            else
                try{
                    audioManager.Play("MainMusic");
                }
                catch(Exception){}
        }
    }
}