using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareGun : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "FlareGun";
        }
    }

    public override bool OnUse()
    {
        return false;
    }
}
