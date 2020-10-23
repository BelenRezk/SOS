using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIView : MonoBehaviour
{
    public Dictionary<string, Vector3> PlayersPossitions { get; set; }
    public Dictionary<string, Vector3> ItemsPossitions { get; set; }


    private void Start()
    {
        PlayersPossitions = new Dictionary<string, Vector3>();
        ItemsPossitions = new Dictionary<string, Vector3>();

    }

    private void OnTriggerStay(Collider other) {
        bool isPlayer = other.CompareTag("Player");
        if (isPlayer)
        {
            //ATTACK PLAYER
            if (PlayersPossitions.ContainsKey(other.name))
            {
                PlayersPossitions[other.name] = other.transform.position;
                Debug.Log("AGREGO PLAYER AL ");
            }
            else
            {
                PlayersPossitions.Add(other.name, other.transform.position);
                Debug.Log("COLISION CON PLAYER");
            }
        }
        bool isItem = other.CompareTag("Item");
        if (isItem)
        {
            //GO TO OBJECT
            if (ItemsPossitions.ContainsKey(other.name))
                ItemsPossitions[other.name] = other.transform.position;
            else
                ItemsPossitions.Add(other.name, other.transform.position);
            Debug.Log("COLISION CON ITEM");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool isPlayer = other.CompareTag("Player");
        if (isPlayer && PlayersPossitions.ContainsKey(other.name))
        {
            PlayersPossitions.Remove(other.name);
            Debug.Log("SALIO DE COLISION CON PLAYER");
        }
        bool isItem = other.CompareTag("Item");
        if (isItem && ItemsPossitions.ContainsKey(other.name))
        {
            ItemsPossitions.Remove(other.name);
            Debug.Log("SALIO DE COLISION CON ITEM");
        }
    }

}
