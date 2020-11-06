using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject owner;
    public int SLOTS = 9;
    public int selectedPosition = 0;
    private int currentNumberOfItems = 0;
    private IInventoryItem[] mItems;
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<Tuple<int, int>> PositionChanged;

    private void Start()
    {
        mItems = new IInventoryItem[SLOTS];
    }

    public void AddItem(IInventoryItem item)
    {
        if (currentNumberOfItems < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                for (int i = 0; i < mItems.Length; i++)
                {
                    if (mItems[i] == null)
                    {
                        mItems[i] = item;
                        break;
                    }
                }
                currentNumberOfItems++;
                item.OnPickup(owner);

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item, selectedPosition));
                    /*Transform inventoryPanel = transform.Find("InventoryPanel");
                    Transform imageTransform = inventoryPanel[selectedPosition].GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    image.color = new Color32(255, 255, 255, 255);*/
                }
            }
        }
    }

    public void ChangeSelectedPosition(int position)
    {
        Tuple<int, int> positions = new Tuple<int, int>(selectedPosition, position);
        if (PositionChanged != null)
        {
            PositionChanged(this, positions);
        }
        selectedPosition = position;
    }

    public void RemoveSelectedItem()
    {
        IInventoryItem item = mItems[selectedPosition];
        if (item != null)
            RemoveItem(item);
    }

    public void UseSelectedItem()
    {
        IInventoryItem item = mItems[selectedPosition];
        if (item != null)
            UseItem(item);

    }
    public void RemoveItem(IInventoryItem item)
    {
        int itemPosition = -1;
        for (int i = 0; i < mItems.Length && itemPosition == -1; i++)
        {
            if (mItems[i] == item)
                itemPosition = i;
        }
        if (itemPosition != -1)
        {
            mItems[itemPosition] = null;
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
                rigidbody.AddForce(xForce, 500f, zForce);
            }
            if (ItemRemoved != null)
                ItemRemoved(this, new InventoryEventArgs(item, selectedPosition));
        }
    }

    private void UseItem(IInventoryItem item)
    {
        int itemPosition = -1;
        for (int i = 0; i < mItems.Length && itemPosition == -1; i++)
        {
            if (mItems[i] == item)
                itemPosition = i;
        }
        if (itemPosition != -1)
        {
            bool usedItem = item.OnUse();
            if (usedItem)
            {
                mItems[itemPosition] = null;
                currentNumberOfItems--;
                Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
                if (collider != null)
                    collider.enabled = true;
                if (ItemRemoved != null)
                    ItemRemoved(this, new InventoryEventArgs(item, selectedPosition));
            }
        }
    }

    public void DropAllItems()
    {
        int position = 0;
        while (currentNumberOfItems != 0 && position < SLOTS)
        {
            IInventoryItem item = mItems[position];
            if (item != null)
                RemoveItem(item);
            position++;
        }
    }

    public void RemoveItem(int position)
    {
        IInventoryItem item = mItems[position];
        if (item != null)
            RemoveItem(item);
    }
}
