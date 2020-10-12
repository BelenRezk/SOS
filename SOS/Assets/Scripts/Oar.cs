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

    public override void OnDrop()
    {
        gameObject.SetActive(true);
        this.transform.parent = null;
        //GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<BoxCollider>().enabled = true;
    }
}
