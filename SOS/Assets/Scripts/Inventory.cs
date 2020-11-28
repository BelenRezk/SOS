using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject owner;
    public int SLOTS = 9;
    public int currentNumberOfItems = 0;
    public List<IInventoryItem> mItems;
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    //public event EventHandler<Tuple<int, int>> PositionChanged;

    private void Start()
    {
        mItems = new List<IInventoryItem>();
    }

    public void AddItem(IInventoryItem item)
    {
        if (currentNumberOfItems < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                currentNumberOfItems++;
                item.OnPickup(owner);

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item, currentNumberOfItems == 1));
                }
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            currentNumberOfItems--;
            item.OnDrop();
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            Rigidbody rigidbody = (item as MonoBehaviour).GetComponent<Rigidbody>();
            if (collider != null)
                collider.enabled = true;
            if(rigidbody != null)
            {
                float xForce = UnityEngine.Random.Range(-80f, 80f);
                float zForce = UnityEngine.Random.Range(-80f, 80f);
                rigidbody.AddForce(xForce, 800f, zForce);
            }
            if (ItemRemoved != null)
                ItemRemoved(this, new InventoryEventArgs(item, currentNumberOfItems == 0));
        }
    }

    public void UseItem()
    {
        UseItem(mItems[0]);
    }

    public void UseItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            bool usedItem = item.OnUse();
            if (usedItem)
            {
                mItems.Remove(item);
                currentNumberOfItems--;
                Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
                if (collider != null)
                    collider.enabled = true;
                if (ItemRemoved != null)
                    ItemRemoved(this, new InventoryEventArgs(item, currentNumberOfItems == 0));
            }
        }
    }

    public void DropAllItems()
    {
        while (currentNumberOfItems != 0)
        {
            IInventoryItem item = mItems[0];
            if (item != null)
                RemoveItem(item);
        }
    }
}
