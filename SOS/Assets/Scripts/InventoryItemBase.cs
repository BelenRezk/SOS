using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public virtual bool HasOwner { get; set; }

    public Sprite _Image;

    public Sprite Image
    {
        get { return _Image; }
    }
    public AudioClip soundClip;
    [HideInInspector]
    public Transform objectDestination;

    public virtual void OnPickup(GameObject player)
    {
        try
        {
            objectDestination = player.GetComponentInChildren<HeldObject>().transform;
            AudioSource.PlayClipAtPoint(soundClip, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception e)
        {
            Debug.Log("No audio clip");
        }
        this.transform.parent = player.transform;
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        /*RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }*/

        Vector3 newPos = new Vector3(1, 1, 1);
        this.transform.position = this.transform.parent.position + newPos;
        HasOwner = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(true);
        this.transform.parent = null;
        GetComponent<BoxCollider>().enabled = true;
    }

    public abstract bool OnUse();
}
