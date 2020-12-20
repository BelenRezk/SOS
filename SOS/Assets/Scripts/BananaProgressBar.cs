using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaProgressBar : MonoBehaviour
{

    public float minimum;
    public float maximum;
    public float current;
    public Image fillMask;
    public Banana banana;
    public ThirdPersonMovement player;

    // Start is called before the first frame update
    void Start()
    {
        minimum = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        maximum = banana.duration;
        current = player.remainingBananaTime;
        GetCurrentFill();
        fillMask.gameObject.SetActive(player.isUsingBanana);    
        fillMask.color = new Color32(0, 255, 0, 100);
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        fillMask.fillAmount = fillAmount;
    }
}
