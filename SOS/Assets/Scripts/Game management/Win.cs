using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    
    public string winScene;

    private void OnTriggerEnter(Collider player)
    {
        int itemCount = player.transform.childCount;
        int winningItemsCount = 0;
        for (int i = 0; i < itemCount; i++)
        {
            IInventoryItem item = player.transform.GetChild(i).GetComponent<IInventoryItem>();
            if (item != null && item.Name != null && (item.Name.Contains("Oar") || item.Name.Equals("FlareGun")
                || item.Name.Equals("EmergencyKit") || item.Name.Equals("Parachute")))
            {
                winningItemsCount ++;
            }
        }
        if (winningItemsCount == 5)
        {
            try
            {
                FindObjectOfType<AudioManager>().Stop("MainMusic");
                FindObjectOfType<AudioManager>().Stop("BananaMusic");
                FindObjectOfType<AudioManager>().Stop("OldLadyAbilityMusic");
                FindObjectOfType<AudioManager>().Stop("HippieAbilityMusic");
                FindObjectOfType<AudioManager>().Stop("RadarBlip");
                FindObjectOfType<AudioManager>().Play("Win");
                FindObjectOfType<AudioManager>().Play("Jungle");
            }
            catch (Exception)
            {
                //there's no music to play or stop
            }
            LoadWinner.winner = "The winner is " + player.name;
            SceneManager.LoadScene("winScene");
        }
        else
        {
            try
            {
                if (player.tag == "Player")
                {
                    Debug.Log("Entered else!");
                    StartCoroutine(player.GetComponent<ThirdPersonMovement>().EnableMessagePanel());
                }
            }
            catch (Exception)
            {
                Debug.Log("Error!");
                //is ai
            }
        }
    }
}
