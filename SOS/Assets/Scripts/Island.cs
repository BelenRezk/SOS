using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        IInventoryItem item = other.collider.GetComponent<Collider>().GetComponent<IInventoryItem>();
        if (item != null)
        {
            item.HasOwner = false;
        }
    }
}
