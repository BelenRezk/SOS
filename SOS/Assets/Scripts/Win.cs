﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    
    public string winScene;

    private void OnTriggerEnter(Collider player)
    {
        //TODO: Revisar por qué el jugador AI no colisiona con el trigger
        int itemCount = player.transform.childCount;
        for (int i = 0; i < itemCount; i++)
        {
            IInventoryItem item = player.transform.GetChild(i).GetComponent<IInventoryItem>();
            if (item != null && item.Name != null && item.Name.Equals("Oar"))
            {
                SceneManager.LoadScene("winScene");
            }
        }
    }
}
