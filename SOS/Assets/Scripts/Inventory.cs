using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 9;
    private int selectedPosition = 0;
    private int currentNumberOfItems = 0;
    private IInventoryItem[] mItems = new IInventoryItem[SLOTS];
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public void AddItem(IInventoryItem item)
    {
        if (currentNumberOfItems < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                for(int i = 0;i<mItems.Length;i++)
                {
                    if(mItems[i] == null)
                    {
                        mItems[i] = item;
                        break;
                    }
                }
                currentNumberOfItems++;
                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void ChangeSelectedPosition(int position)
    {
        selectedPosition = position;
    }

    public void RemoveSelectedItem()
    {
        IInventoryItem item = mItems[selectedPosition];
        if(item != null)
            RemoveItem(item);
    }
    public void RemoveItem(IInventoryItem item)
    {
        int itemPosition = -1;
        for(int i = 0; i<mItems.Length && itemPosition == -1;i++)
        {
            if(mItems[i] == item)
                itemPosition = i;
        }
        if(itemPosition != -1)
        {
            mItems[itemPosition] = null;
            currentNumberOfItems--;
            item.OnDrop();
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider != null)
            {
                collider.enabled = true;
            }
            if(ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}
