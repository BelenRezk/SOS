using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIView : MonoBehaviour
{
    public List<(string, Vector3)> PlayersPositions { get; set; }
    public List<(string, Vector3)> ItemsPositions { get; set; }
    public CharacterDifferentiationBase characterBehaviour;


    private void Start()
    {
        PlayersPositions = new List<(string, Vector3)>();
        ItemsPositions = new List<(string, Vector3)>();

    }

    private void OnTriggerStay(Collider other) {
        bool isPlayer = other.CompareTag("Player");
        string aiTag = transform.parent.name;
        AIItemsInteraction aiPlayer = this.GetComponentInParent<AIItemsInteraction>();
        if (isPlayer)
        {
            if(other.name != aiTag){

                AIPowerUps aiPowerUps = this.GetComponentInParent<AIPowerUps>();
                aiPowerUps.hasClosePlayer = true;
                if(aiPlayer.abilityCooldownRemaining <= 0 && !aiPlayer.abilityActive)
                {
                    characterBehaviour.UseSpecialAbility();
                    aiPlayer.abilityDurationRemaining = aiPlayer.abilityDuration;
                    aiPlayer.abilityActive = true;
                }
               
                if (aiPlayer.abilityActive && aiPlayer.abilityDurationRemaining <= 0)
                {
                    characterBehaviour.FinishSpecialAbility();
                    aiPlayer.abilityActive = false;
                    aiPlayer.abilityCooldownRemaining = aiPlayer.abilityCooldown;
                }

                //Found player to attack
                
                if (aiPlayer.coconutInventory.currentNumberOfItems > 0){
                    aiPlayer.transform.LookAt(other.transform);
                }
                if (PlayersPositions.FindAll(p => p.Item1 == other.name).Count > 0)
                {
                    var playerToUpdate = PlayersPositions.Find(p => p.Item1 == other.name);
                    PlayersPositions.Remove(playerToUpdate);
                }
                PlayersPositions.Add((other.name, other.transform.position));
            }
        }
        bool isItem = other.CompareTag("Item");
        if (isItem)
        {
            if (ItemsPositions.FindAll(a => a.Item1 == other.name).Count == 0)
            {
                ItemsPositions.Add((other.name, other.transform.position));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isPlayer = other.CompareTag("Player");

        if (isPlayer && PlayersPositions.FindAll(p => p.Item1 == other.name).Count > 0)
        {
            AIPowerUps aiPlayer = this.GetComponentInParent<AIPowerUps>();
            aiPlayer.hasClosePlayer = false;
            var playerToRemove = PlayersPositions.Find(p => p.Item1 == other.name);   
            PlayersPositions.Remove(playerToRemove);
        }
    }

}
