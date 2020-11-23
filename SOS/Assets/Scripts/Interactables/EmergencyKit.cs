using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyKit : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "EmergencyKit";
        }
    }

    public override bool WinItem
    {
        get
        {
            return true;
        }
    }

    public override bool OnUse()
    {
        return false;
    }
}

