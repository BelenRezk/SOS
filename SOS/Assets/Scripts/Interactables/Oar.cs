using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oar : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Oar";
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
