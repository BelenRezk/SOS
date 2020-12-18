using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public ThirdPersonMovement player;

    // Start is called before the first frame update
    void Start()
    {
        minimum = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        maximum = player.abilityCooldown;
        current = maximum - player.abilityCooldownRemaining;
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }
}
