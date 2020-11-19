using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Parachute";
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
