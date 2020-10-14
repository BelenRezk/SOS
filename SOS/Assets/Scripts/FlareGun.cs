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

    public override void OnDrop()
    {
        gameObject.SetActive(true);
        this.transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
    }

    public override bool OnUse()
    {
        return false;
    }
}
