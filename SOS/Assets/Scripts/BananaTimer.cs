using System;
using UnityEngine;
using UnityEngine.UI;

public class BananaTimer : MonoBehaviour
{

    public ThirdPersonMovement character;
    private Text timerUI;

    void Start()
    {
        timerUI = GetComponent<Text>();
    }

    void Update()
    {
        timerUI.text = "banana: ";
        float timer = character.remainingBananaTime;
        if (timer > 0)
        {
            timerUI.enabled = true;
            TimeSpan spanTime = TimeSpan.FromSeconds(timer);
            timerUI.text += spanTime.Seconds.ToString("00");
        }
        else
            timerUI.enabled = false;
    }
}