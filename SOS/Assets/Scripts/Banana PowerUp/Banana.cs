﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : InventoryItemBase
{
    public BananaManager bananaManager;
    public override string Name
    {
        get
        {
            return "Banana";
        }
    }

    public float duration = 5f;

    public override void OnUse()
    {
        bananaManager.Use(this.transform.parent);

        Transform player = this.transform.parent;

        GameObject playerGO = player.gameObject;

        this.transform.parent = null;

        try
        {
            ThirdPersonMovement thirdPersonMovement = playerGO.GetComponent<ThirdPersonMovement>();
            thirdPersonMovement.UseBanana(duration);
        }
        catch (Exception e)
        {
            AIMovement aiMovement = playerGO.GetComponent<AIMovement>();
            aiMovement.UseBanana(duration);
        }
    }
}

