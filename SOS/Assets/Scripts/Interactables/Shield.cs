using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Shield";
        }
    }

    public bool _hasOwner;
    public override bool HasOwner
    {
        get
        {
            return _hasOwner;
        }
        set
        {
            _hasOwner = value;
        }
    }

    public override bool WinItem
    {
        get
        {
            return false;
        }
    }

    public override bool OnUse()
    {
        Transform player = this.transform.parent;
        GameObject playerGO = player.gameObject;
        bool shieldUsed;
        try
        {
            ThirdPersonMovement thirdPersonMovement = playerGO.GetComponent<ThirdPersonMovement>();
            shieldUsed = thirdPersonMovement.UseShield();
        }
        catch (Exception)
        {
            AIMovement aiMovement = playerGO.GetComponent<AIMovement>();
            shieldUsed = aiMovement.UseShield();
        }
        if (shieldUsed)
            this.transform.parent = null;
        return true;
    }
}