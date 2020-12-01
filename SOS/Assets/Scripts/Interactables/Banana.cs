using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Banana";
        }
    }
    public override bool WinItem
    {
        get
        {
            return false;
        }
    }

    public float duration = 5f;

    public override bool OnUse()
    {
        Transform player = this.transform.parent;
        GameObject playerGO = player.gameObject;
        try
        {
            ThirdPersonMovement thirdPersonMovement = playerGO.GetComponent<ThirdPersonMovement>();
            if(!thirdPersonMovement.isUsingBanana)
            {
                this.transform.parent = null;
                thirdPersonMovement.UseBanana(duration);
                FindObjectOfType<AudioManager>().Stop("MainMusic");
                if(!thirdPersonMovement.abilityActive)
                    FindObjectOfType<AudioManager>().Play("BananaMusic");
            }
            else
                return false;
        }
        catch  (Exception)
        {
            //Main player is not using banana
        }
        return true;
    }
}

