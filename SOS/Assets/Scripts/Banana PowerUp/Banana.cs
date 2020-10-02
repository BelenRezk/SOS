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

    public override void OnDrop()
    {
        bananaManager.Use(this.transform.parent);
        this.transform.parent = null;
    }
}

