using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public float minimum;
    public float maximum;
    public float current;
    public Image oldLadyMask;
    public Image pilotMask;
    public Image businessWomanMask;
    public Image hippieMask;
    public Image fillMask;
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
        Transform fill = this.transform.Find("Fill");
        GetCurrentFill();
        string name = "";
        switch (player.gameObject.name)
        {
            case "Old Lady":
                name = "MaskOldLady";
                break;
            case "Pilot":
                name = "MaskPilot";
                break;
            case "Businesswoman":
                name = "MaskBusinessWoman";
                break;
            case "Hippie":
                name = "MaskHippie";
                break;
            default:
                name = "MaskPilot";
                break;
        }
        GameObject mask = fill.Find(name).gameObject;
        mask.SetActive(true);
        if (current >= maximum && !player.abilityActive)
            fillMask.color = new Color32(0, 255, 0, 100);
        else
            fillMask.color = new Color32(255, 255, 255, 100);
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        fillMask.fillAmount = fillAmount;
    }
}
