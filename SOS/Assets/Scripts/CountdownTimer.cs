using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public string gameOverScene;
    private float timer = 70f;
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
                SceneManager.LoadScene("gameOverScene");
            }
        }
    }
}
