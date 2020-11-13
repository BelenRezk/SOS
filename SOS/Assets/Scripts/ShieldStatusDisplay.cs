using System;
using UnityEngine;
using UnityEngine.UI;

public class ShieldStatusDisplay : MonoBehaviour
{

    public ThirdPersonMovement character;
    private Text textUI;

    public string text;

    void Start()
    {
        textUI = GetComponent<Text>();
        textUI.text = text;
    }

    void Update()
    {
        bool isShieldActive = character.hasShield;
        if (isShieldActive)
            textUI.enabled = true;
        else
            textUI.enabled = false;
    }
}