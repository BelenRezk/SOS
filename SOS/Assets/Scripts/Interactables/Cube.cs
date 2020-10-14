using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Cube";
        }
    }

    public override bool OnUse()
    {
        return false;
    }
}
