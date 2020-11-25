using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public string gameOverScene;
    private float timer = 300f;
    private Text timerUI;
    
    void Start()
    {
        timerUI = GetComponent<Text>();    
    }

    void Update()
    {
        if(timer >= 0)
        {
            TimeSpan spanTime = TimeSpan.FromSeconds(timer);
            timerUI.text = spanTime.Minutes + " : " + spanTime.Seconds.ToString("00");
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                try{
                FindObjectOfType<AudioManager>().Stop("MainMusic");
                FindObjectOfType<AudioManager>().Stop("BananaMusic");
                FindObjectOfType<AudioManager>().Stop("OldLadyAbilityMusic");
                FindObjectOfType<AudioManager>().Stop("HippieAbilityMusic");
                FindObjectOfType<AudioManager>().Stop("RadarBlip");
                FindObjectOfType<AudioManager>().Play("GameOver");
                FindObjectOfType<AudioManager>().Play("Jungle");
                }
                catch(Exception){
                    //there is no music to stop
                }
                SceneManager.LoadScene("gameOverScene");
            }
        }
    }
}
