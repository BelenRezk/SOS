using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    private GameManager gameManager;
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

    public virtual bool WinItem { get; }
    public AudioClip soundClip;
    [HideInInspector]
    public Transform objectDestination;

    public int itemId;

    private void Start()
    {
        gameManager = GameManager.instance;
        itemId = gameManager.ItemsId;
        gameManager.ItemsId++;
    }

    public virtual void OnPickup(GameObject player)
    {
        if (Name.Equals("Coconut")) {
            try
            {
                AIItemsInteraction ai = player.GetComponent<AIItemsInteraction>();
                ai.coconutCount++;
            }
            catch (Exception e)
            {
                //is not AI
            }
        }
        try
        {
            objectDestination = player.GetComponentInChildren<HeldObject>().transform;
            AudioSource.PlayClipAtPoint(soundClip, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        }
        catch (Exception)
        {
            Debug.Log("No audio clip");
        }
        this.transform.parent = player.transform;
        HasOwner = true;
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        Vector3 newPos = new Vector3(1, 1, 1);
        try
        {
            this.transform.position = this.transform.parent.position + newPos;
            HasOwner = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.SetActive(true);
            this.transform.parent = null;
            GetComponent<BoxCollider>().enabled = true;
        }
        catch (Exception)
        {
            //TODO: DOES NOT HAVE PARENT
        }
        
    }

    public abstract bool OnUse();
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public override bool Equals(object obj)
    {
        return itemId == ((InventoryItemBase)obj).itemId;
    }

    public override int GetHashCode()
    {
        return itemId;
    }
}
