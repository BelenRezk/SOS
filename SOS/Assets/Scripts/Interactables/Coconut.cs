using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : InventoryItemBase
{
    public float throwSpeed = 2000f;
    public override string Name
    {
        get
        {
            return "Coconut";
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

    public override bool OnUse()
    {
        gameObject.SetActive(true);
        this.transform.parent = null;

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<SphereCollider>().enabled = true;
        this.transform.position = objectDestination.position;

        GetComponent<Rigidbody>().AddForce(objectDestination.forward * throwSpeed);
        return true;
    }
}
