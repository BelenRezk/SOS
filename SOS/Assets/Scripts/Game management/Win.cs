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
            if (item != null && item.Name != null && (item.Name.Equals("Oar") || item.Name.Equals("FlareGun")))
            {
                winningItemsCount ++;
            }
        }
        if(winningItemsCount == 2)
        {
            FindObjectOfType<AudioManager>().Stop("MainMusic");
            FindObjectOfType<AudioManager>().Stop("BananaMusic");
            FindObjectOfType<AudioManager>().Stop("OldLadyAbilityMusic");
            FindObjectOfType<AudioManager>().Stop("HippieAbilityMusic");
            FindObjectOfType<AudioManager>().Stop("RadarBlip");
            FindObjectOfType<AudioManager>().Play("Win");
            FindObjectOfType<AudioManager>().Play("Jungle");
            LoadWinner.winner = "The winner is " + player.name;
            SceneManager.LoadScene("winScene");
        }
    }
}
