using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory CoconutInventory;
    public Inventory PowerUpInventory;
    public Inventory WinItems;

    void Start()
    {
        CoconutInventory.ItemAdded += InventoryScript_ItemAdded;
        CoconutInventory.ItemRemoved += Inventory_ItemRemoved;
        //Inventory.PositionChanged += Inventory_PositionChanged;

        PowerUpInventory.ItemAdded += InventoryScript_ItemAdded;
        PowerUpInventory.ItemRemoved += Inventory_ItemRemoved;

        WinItems.ItemAdded += InventoryScript_ItemAdded;
        WinItems.ItemRemoved += Inventory_ItemRemoved;
        //WinItems.PositionChanged += Inventory_PositionChanged;
        
    }

    private void InitInventory()
    {
        //Transform inventoryPanel = transform.Find("InventoryPanel");
        Transform winItems = transform.Find("WinItems");
        /*foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            if (position == 0)
                image.color = new Color32(255, 255, 255, 255);
            else
                image.color = new Color32(120, 120, 120, 140);
            position++;
        }*/
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
            inventoryPanel = transform.Find("WinItems");
        else if(e.Item.Name == "Coconut")
            inventoryPanel = transform.Find("CoconutPanel");
        else
            inventoryPanel = transform.Find("PowerUpPanel");
        if(e.Item.WinItem)
        {
            foreach (Transform slot in inventoryPanel)
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                if (!image.enabled)
                {
                    image.enabled = true;
                    image.sprite = e.Item.Image;
                    if(itemDragHandler != null)
                        itemDragHandler.Item = e.Item;
                    break;
                }
            }
        }
        else
        {
            if(e.ShouldUpdateSprite)
            {
                foreach (Transform slot in inventoryPanel)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                    image.enabled = true;
                    image.sprite = e.Item.Image;
                    if(itemDragHandler != null)
                        itemDragHandler.Item = e.Item;
                    break;
                }   
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel;
        if (e.Item.WinItem)
            inventoryPanel = transform.Find("WinItems");
        else if(e.Item.Name == "Coconut")
            inventoryPanel = transform.Find("CoconutPanel");
        else
            inventoryPanel = transform.Find("PowerUpPanel");
        if(e.Item.WinItem || e.ShouldUpdateSprite)
        {
            foreach (Transform slot in inventoryPanel)
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                image.enabled = false;
                image.sprite = null;

                if (itemDragHandler.Item != null && itemDragHandler.Item.Equals(e.Item))
                {
                    //image.enabled = false;
                    //image.sprite = null;
                    itemDragHandler.Item = null;
                    break;
                }
            }
        }
    }

    /*private void Inventory_PositionChanged(object sender, Tuple<int, int> positions)
    {
        int previousPosition = positions.Item1;
        int newPosition = positions.Item2;
        if (previousPosition != newPosition)
        {
            Transform inventoryPanel = transform.Find("InventoryPanel");
            int position = 0;
            foreach (Transform slot in inventoryPanel)
            {
                if (position == newPosition)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    image.color = new Color32(255, 255, 255, 255);
                }
                if (position == previousPosition)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    Image image = imageTransform.GetComponent<Image>();
                    image.color = new Color32(120, 120, 120, 140);
                }
                position++;
            }
        }
    }*/
}
