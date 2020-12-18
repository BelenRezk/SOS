using UnityEngine;
using System;
using System.Collections;

public class HippieBehaviour : CharacterDifferentiationBase
{
    public float xRange = 100f;
    public float yRange = 50f;
    public float zRange = 100f;
    public HippieBehaviour(MovementBase movementBase, bool playMusic, AudioManager manager)
    {
        movement = movementBase;
        shouldPlayMusic = playMusic;
        audioManager = manager;
        movement.abilityCooldown = 40f;
        movement.abilityDuration = 2f;
    }

    public override void UseSpecialAbility()
    {
        Debug.Log("Hippie Ability");
        if (shouldPlayMusic)
        {
            try{
            audioManager.Stop("MainMusic");
            audioManager.Stop("BananaMusic");
            audioManager.Play("HippieAbilityMusic");
            }
            catch(Exception){}
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            MovementBase movementScript = GetMovementScript(player);
            if (movementScript != null && !movementScript.Equals(movement) && IsInRange(movementScript))
            {
                if(!shouldPlayMusic && movementScript is ThirdPersonMovement)
                    try{
                        audioManager.Play("HippieLaugh");
                    }
                    catch(Exception){}
                movementScript.coconutInventory.DropAllItems();
                movementScript.powerUpInventory.DropAllItems();
                if (movementScript.winItems != null)
                    movementScript.winItems.DropAllItems();
            }
        }
    }

    private MovementBase GetMovementScript(GameObject playerGO)
    {
        MovementBase player;
        player = playerGO.GetComponent<ThirdPersonMovement>();
        if (player == null)
            player = playerGO.GetComponent<AIItemsInteraction>();
        return player;
    }

    private bool IsInRange(MovementBase movementScript)
    {
        float xDifference = Math.Abs(movement.transform.position.x - movementScript.transform.position.x);
        float yDifference = Math.Abs(movement.transform.position.y - movementScript.transform.position.y);
        float zDifference = Math.Abs(movement.transform.position.z - movementScript.transform.position.z);
        return xDifference <= xRange && yDifference <= yRange && zDifference <= zRange;
    }

    public override void FinishSpecialAbility()
    {
        if (shouldPlayMusic)
        {
            try{
            audioManager.Stop("HippieAbilityMusic");
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
        Debug.Log("Finish Hippie Ability");
    }
}