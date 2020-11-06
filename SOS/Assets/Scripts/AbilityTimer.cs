using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimer : MonoBehaviour
{

    public ThirdPersonMovement character;
    private Text timerUI;

    void Start()
    {
        timerUI = GetComponent<Text>();
    }

    void Update()
    {
        timerUI.text = "Ability: ";
        float timer = character.abilityCooldownRemaining;
        if (character.abilityActive)
            timerUI.text += "In use";
        else
        {
            if (timer >= 0)
            {
                TimeSpan spanTime = TimeSpan.FromSeconds(timer);
                timerUI.text += spanTime.Seconds.ToString("00");
            }
            else
                timerUI.text += "Ready";
        }
    }
}
