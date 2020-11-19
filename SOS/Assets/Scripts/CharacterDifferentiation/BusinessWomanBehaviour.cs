using UnityEngine;
using System;

public class BusinessWomanBehaviour : CharacterDifferentiationBase
{
    public float xRange = 10f;
    public float yRange = 5f;
    public float zRange = 10f;

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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        MovementBase playerToStealFrom = null;
        float playerToStealFromDistance = float.MaxValue;
        foreach (GameObject player in players)
        {
            MovementBase movementScript = GetMovementScript(player);
            if (movementScript != null && !movementScript.Equals(movement))
            {
                if(IsClosestInRangeWithWinItem(player, movementScript, playerToStealFrom, playerToStealFromDistance))
                {
                    playerToStealFrom = movementScript;
                    playerToStealFromDistance = Vector3.Distance(movement.transform.position, movementScript.transform.position);
                }
            }
        }

        if (shouldPlayMusic && playerToStealFrom != null)
        {
            audioManager.Stop("MainMusic");
            audioManager.Stop("BananaMusic");
            audioManager.Play("MoneySound");
            audioManager.Play("BusinessWomanLaugh");
            StealWinItem(playerToStealFrom);
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

    private bool IsClosestInRangeWithWinItem(GameObject player, MovementBase movementScript, MovementBase playerToStealFrom, float playerToStealFromDistance)
    {
        float xDifference = Math.Abs(movement.transform.position.x - movementScript.transform.position.x);
        float yDifference = Math.Abs(movement.transform.position.y - movementScript.transform.position.y);
        float zDifference = Math.Abs(movement.transform.position.z - movementScript.transform.position.z);
        bool isInRange = xDifference <= xRange && yDifference <= yRange && zDifference <= zRange;
        bool isClosest = Vector3.Distance(movement.transform.position, movementScript.transform.position) < playerToStealFromDistance;
        bool HasWinItem = movementScript.winItems.currentNumberOfItems > 0;
        return isInRange && isClosest && HasWinItem;
    }

    private void StealWinItem(MovementBase playerToStealFrom)
    {
        IInventoryItem item = GetFirstWinItem(playerToStealFrom); 
        playerToStealFrom.winItems.RemoveItem(item);
        movement.winItems.AddItem(item);
    }

    private IInventoryItem GetFirstWinItem(MovementBase playerToStealFrom)
    {
        IInventoryItem item = null;
        foreach(IInventoryItem winItem in playerToStealFrom.winItems.mItems)
        {
            if(winItem != null && item == null)
                item = winItem;
        }
        return item;
    }

    public override void FinishSpecialAbility()
    {
        if (shouldPlayMusic)
        {
            if (movement.isUsingBanana)
                audioManager.Play("BananaMusic");
            else
                audioManager.PlayMainMusic();
        }
        Debug.Log("Finish Business Woman Ability");
    }
}