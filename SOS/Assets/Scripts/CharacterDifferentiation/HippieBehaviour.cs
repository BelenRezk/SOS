using UnityEngine;
using System;
using System.Collections;

public class HippieBehaviour : CharacterDifferentiationBase
{
    public float xRange = 10f;
    public float yRange = 5f;
    public float zRange = 10f;
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
            audioManager.Stop("MainMusic");
            audioManager.Stop("BananaMusic");
            audioManager.Play("HippieAbilityMusic");
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            MovementBase movementScript = GetMovementScript(player);
            if (movementScript != null && !movementScript.Equals(movement) && IsInRange(movementScript))
            {
                movementScript.inventory.DropAllItems();
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
            audioManager.Stop("HippieAbilityMusic");
            if (movement.isUsingBanana)
                audioManager.Play("BananaMusic");
            else
                audioManager.Play("MainMusic");
        }
        Debug.Log("Finish Hippie Ability");
    }
}