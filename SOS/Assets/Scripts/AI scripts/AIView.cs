using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIView : MonoBehaviour
{
    public List<(string, Vector3)> PlayersPositions { get; set; }
    public List<(string, Vector3)> ItemsPositions { get; set; }


    private void Start()
    {
        PlayersPositions = new List<(string, Vector3)>();
        ItemsPositions = new List<(string, Vector3)>();

    }

    private void OnTriggerStay(Collider other) {
        bool isPlayer = other.CompareTag("Player");
        if (isPlayer)
        {
            //TODO: Change name to each AI player
            if(other.name != "AI Player"){
                //Found player to attack
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
            var playerToRemove = PlayersPositions.Find(p => p.Item1 == other.name);   
            PlayersPositions.Remove(playerToRemove);
        }
    }

}
